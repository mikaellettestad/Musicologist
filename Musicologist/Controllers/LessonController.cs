﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musicologist.Models;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services.Interfaces;
using Musicologist.ViewModels;
using System.Linq;

namespace Musicologist.Controllers
{
    public class LessonController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILessonRepository _repository;
        private readonly ILessonService _service;

        public LessonViewModel Model { get; set; }
        public LessonController(UserManager<ApplicationUser> userManager, ILessonRepository repository, ILessonService service)
        {
            _userManager = userManager;
            _repository = repository;
            _service = service;
        }

        public IActionResult Index(int lessonId, int courseId, int i, bool lastLesson, int numberOfLessons)
        {
            Model = GetLesson(_userManager.GetUserId(User), lessonId);

            if(Model != null)
            {
                Model.NumberOfLessons = numberOfLessons;

                Model.CourseId = courseId;

                if (lastLesson)
                {
                    Model.IsLastLesson = true;

                    return View(Model);
                }

                Model.NextLessonIndex = i + 1;

                Model.NextLessonId = _service.GetNextLessonId(courseId, i);

                return View(Model);
            }

            return new StatusCodeResult(404);
        }

        private LessonViewModel GetLesson(string applicationUserId, int lessonId)
        {
            var model = _repository.GetLesson(lessonId).Select(lesson => new LessonViewModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Texts = lesson.LessonTexts.Select(lesson => new LessonViewModel.LessonText
                {
                    Title = lesson.Title,
                    Text = lesson.Text,
                }).ToList(),
                Images = lesson.LessonImages.Select(lesson => new LessonViewModel.LessonImage
                {
                    Title = lesson.Title,
                    ImageUrl = lesson.ImageUrl
                }).ToList(),
                AssignmentId = lesson.Assignment.Id
            }).SingleOrDefault();

            if(model != null)
            {
                var applicationUserAssignment = _repository.GetAssignment(applicationUserId, model.AssignmentId).SingleOrDefault();

                if (applicationUserAssignment != null)
                        model.IsCompleted = true;
                    else
                        model.IsCompleted = false;

                    return model;                
            }

            return null;
        }

        private int GetNextLessonId()
        {

            return 1;
        }
    }
}
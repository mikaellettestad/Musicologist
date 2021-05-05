using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Musicologist.Models;

namespace Musicologist.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursePart> CourseParts { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonImage> LessonImages { get; set; }
        public DbSet<LessonText> LessonTexts { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ApplicationUserCourse> ApplicationUserCourses { get; set; }
        public DbSet<ApplicationUserAssignment> ApplicationUserAssignments { get; set; }
    }
}
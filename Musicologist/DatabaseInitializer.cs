using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Musicologist.Data;
using Musicologist.Models;
using System;
using System.Linq;

namespace Musicologist
{
    public class DatabaseInitializer
    {
		public void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			context.Database.Migrate();

			context.Database.EnsureCreated();

			AddRole(context, "Admin");

			AddRole(context, "User");

			string admin = "Admin";

			string student = "User";

			AddUser(userManager, "admin@admin.com", admin);

			AddUser(userManager, "user@user.com", student);
		}

		private void AddRole(ApplicationDbContext context, string role)
		{
			if (context.Roles.Any(r => r.Name == role))
				return;

			context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role });

			context.SaveChanges();
		}

		private void AddUser(UserManager<ApplicationUser> userManager, string user, string role)
		{
			if (userManager.FindByEmailAsync(user).Result == null)
			{
				var identityUser = new ApplicationUser
				{
					UserName = user,
					Email = user,
					EmailConfirmed = true
				};

				var result = userManager.CreateAsync(identityUser, "tsigolocisuM1!").Result;

				if (result.Succeeded)
				{
					userManager.AddToRoleAsync(identityUser, role).Wait();
				}
			}
		}

		private void AddCourses(UserManager<ApplicationUser> userManager, string user, string role)
		{
			throw new NotImplementedException();
		}
	}
}
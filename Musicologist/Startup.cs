using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Musicologist.Data;
using Musicologist.Models;
using Musicologist.Repositories;
using Musicologist.Repositories.Interfaces;
using Musicologist.Services;
using Musicologist.Services.Interfaces;

namespace Musicologist
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            
            services.AddRazorPages();

            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            
            services.AddTransient<IAssignmentRepository, AssignmentRepository>();
            
            services.AddTransient<IApplicationUserCourseRepository, ApplicationUserCourseRepository>();
            
            services.AddTransient<ILessonRepository, LessonRepository>();
            
            services.AddTransient<IAssignmentService, AssignmentService>();
            
            services.AddTransient<ILessonService, LessonService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseDatabaseErrorPage();
            } 
            else
            {
                app.UseExceptionHandler();
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using MyBlogApp.Entity;
using MyBlogApp.Identity;

namespace MyBlogApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddControllersWithViews();

            services.AddDbContext<BlogContext>(x =>
                          x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<IdentityDataContext>(x =>
                          x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppIdentityUser, AppIdentityRole>(x =>
            {}).AddEntityFrameworkStores<IdentityDataContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {              
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;              
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;            
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(_ =>
            {
                
                _.LoginPath = "/Auth/Login";
                _.LogoutPath = "/Auth/Logout";
                _.AccessDeniedPath = "/Auth/AccessDenied"; 
                _.SlidingExpiration = true;
                _.Cookie = new CookieBuilder
                {
                                                                    /////////GÜVENLÝÐÝ AYARLAAAAAAAAAAAAAAAAAAAAAAAAA
                    Name = "asdasdasasdasdasdasdasd",
                    HttpOnly = false,
                    Path="/",
                    SameSite=SameSiteMode.Strict,
                    SecurePolicy=CookieSecurePolicy.SameAsRequest
                };
            });

            services.AddRazorPages();                    
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddAutoMapper(typeof(Startup));
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //geliþtirici moddan çýkar
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {            
                app.UseHsts();
            }            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "blog",
                     pattern: "{controller=blog}/{action=Details}/{content?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
          
        }
    }
}

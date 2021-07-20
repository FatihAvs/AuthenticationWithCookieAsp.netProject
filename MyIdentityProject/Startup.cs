using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyIdentityProject.CustomValidation;
using MyIdentityProject.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using MyIdentityProject.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject
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
            services.AddDbContext<AppIdentityDbContext>(opts =>
            opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"])
            
            ) ;
            services.AddDbContext<PhotosDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("PhotosDbContext")));


            services.AddIdentity<AppUser, IdentityRole>(opts=>
            {
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcçdefgðhiýjklmnöopqrstuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-. _ ";
                opts.Password.RequiredLength = 8;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
              
                }
            ).AddErrorDescriber<CustomIdentitiyErrorDescriber>().AddUserValidator<CustomUserValidator>().AddPasswordValidator<CustomPasswordValidator>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "MyBlog";
            cookieBuilder.HttpOnly = false;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            cookieBuilder.SameSite = SameSiteMode.Strict;

            services.ConfigureApplicationCookie(opts =>
            {
                opts.Cookie = cookieBuilder;
                opts.LoginPath = new PathString("/Home/Login");
                opts.LogoutPath = new PathString("/Member/LogOut");
                opts.SlidingExpiration = true;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
            }
            );

            services.AddMvc(option => option.EnableEndpointRouting = false);
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();
          
        }
    }
}

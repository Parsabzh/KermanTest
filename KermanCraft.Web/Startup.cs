using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KermanCraft.Domain.Models.People;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.IoC;
using NToastNotify;
using KermanCraft.Infrastructure.Localize.IdentityMessage;
using KermanCraft.Domain.Models.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace KermanCraft.Web
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
            services.AddLocalization(options => options.ResourcesPath ="Resources");
           
            services.AddDbContext<KermanCraftDb>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<People, IdentityRole>(options =>
                 {
                     options.SignIn.RequireConfirmedAccount = false;
                     options.SignIn.RequireConfirmedPhoneNumber = false;
                     options.Password.RequireDigit = false;
                     options.Password.RequireLowercase = false;
                     options.Password.RequireUppercase = false;
                     options.Password.RequiredUniqueChars = 0;
                     options.Password.RequireNonAlphanumeric = false;
                     options.Password.RequiredLength = 4;
                     options.SignIn.RequireConfirmedEmail = false;
                     
                     
                 })
                .AddEntityFrameworkStores<KermanCraftDb>().AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();
            services.AddAuthentication().AddCookie(o => o.Cookie.Expiration  = TimeSpan.FromMinutes(30));
            services.AddSession(o =>
            {

                //o.Cookie.Expiration = TimeSpan.FromMinutes(30);
                o.IOTimeout = TimeSpan.FromMinutes(30);
                o.IdleTimeout = TimeSpan.FromMinutes(30);
                o.Cookie.IsEssential = true;
            });
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(10);
                
            });
            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = new PathString("/Admin/Admin/Login");
                option.ExpireTimeSpan=TimeSpan.FromMinutes(30);
                option.Cookie.Name = "KermanCrafts";
                option.Cookie.HttpOnly = true;

                //option.Cookie.Expiration=TimeSpan.FromHours(120);

                //option.AccessDeniedPath = new PathString("/Account/Account/AccessDenied");
            });
            services.AddAntiforgery(option => option.Cookie.Expiration = TimeSpan.Zero);
            services.AddMvc(option => option.EnableEndpointRouting = true).
                AddRazorPagesOptions(option=>option.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute())).
                AddNToastNotifyToastr(new ToastrOptions()
            {
                Title = "پیام",
                ProgressBar = true,
                PositionClass = ToastPositions.BottomRight,
                Rtl = true,

            }).AddViewLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            }).AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(ShareResource));
            });
            
            RegisterServices(services);
            services.AddControllersWithViews();
            services.AddRazorPages();
            
        }
        public static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("fa-IR"),
                new CultureInfo("en-US")
            };
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
            };
            app.UseRequestLocalization(options);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapAreaControllerRoute("Admin","Admin", "Admin/{controller=Admin}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("Artist","Artist", "Artist/{controller=Index}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("Customer","Customer", "Customer/{controller=Index}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });


            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "MyArea",
            //        template: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
              
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

        }
     
    }
}

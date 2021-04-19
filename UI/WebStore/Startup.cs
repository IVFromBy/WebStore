using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.Clients.Employees;
using WebStore.Clients.Values;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entites.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Middleware;
using WebStore.Infrastructure.Services;
using WebStore.Infrastructure.Services.InCookies;
using WebStore.Infrastructure.Services.InMemory;
using WebStore.Infrastructure.Services.InSql;
using WebStore.Interfaces.TestAPI;

namespace WebStore
{
    public record Startup(IConfiguration Configuration)
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt => 
                  opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                  .UseLazyLoadingProxies()
                  );
            services.AddTransient<WebStoreDbInitializer>();

            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            services.AddTransient<IEmployeesData, EmployeesClient>();
            services.AddTransient<IProductData, SqlProductData>();
            services.AddTransient<ICartService, InCookiesCartService>();
            services.AddTransient<IOrderService, SqlOrderService>();
            services.AddTransient<IValuesService, ValuesClient>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
           {
#if DEBUG
               opt.Password.RequireDigit = false;
               opt.Password.RequiredLength = 3;
               opt.Password.RequireLowercase = false;
               opt.Password.RequireUppercase = false;
               opt.Password.RequireNonAlphanumeric = false;
               opt.Password.RequiredUniqueChars = 3;
#endif
               opt.User.RequireUniqueEmail = false;
               opt.User.AllowedUserNameCharacters = "QqwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLzZxXcCvVbBnNmM1234567890_";

               opt.Lockout.AllowedForNewUsers = false;
               opt.Lockout.MaxFailedAccessAttempts = 10;
               opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
           });

            services.ConfigureApplicationCookie(opt =>
           {
               opt.Cookie.Name = "WebStore.GB";
               opt.Cookie.HttpOnly = true;
               opt.ExpireTimeSpan = TimeSpan.FromDays(10);

               opt.LoginPath = "/Account/Login";
               opt.LogoutPath = "/Account/Logout";
               opt.AccessDeniedPath = "/Account/AccessDenied";

               opt.SlidingExpiration = true;
           });
            services
                .AddControllersWithViews(/*opt => opt.Conventions.Add(new TestControllerModelConventions())*/)
                .AddRazorRuntimeCompilation();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db)
        {
            db.Initiallize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseWelcomePage("/wel");

            app.UseMiddleware<TestMiddleware>();

            app.MapWhen(
                 context => context.Request.Query.ContainsKey("id") && context.Request.Query["id"] == "5",
                 context => context.Run(async request => await request.Response.WriteAsync("Hell id = 5!!!"))
                );

            app.Map("/hhh", context => context.Run(async request => await request.Response.WriteAsync("Hell!!!")));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Employees;
using WebStore.Clients.Identity;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Values;
using WebStore.Domain.Entites.Identity;
using WebStore.Hubs;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Middleware;
using WebStore.Infrastructure.Services.InCookies;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestAPI;
using WebStore.Logger;
using WebStore.Services.Services;
using WebStore.Services.Services.InCookies;

namespace WebStore
{
    public record Startup(IConfiguration Configuration)
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeesData, EmployeesClient>();
            services.AddTransient<IProductData, ProductsClient>();
            //services.AddTransient<ICartService, InCookiesCartService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICartStore, InCookiesCartStore>();
            services.AddTransient<IOrderService, OrdersClient>();
            services.AddTransient<IValuesService, ValuesClient>();

            services.AddIdentity<User, Role>()
                .AddIdentityWebStoreWebAPIClient()
                .AddDefaultTokenProviders();

            //services.AddIdentityWebStoreWebAPIClient();
            //#region Связка identity
            //services.AddTransient<IUserStore<User>, UsersClient>();
            //services.AddTransient<IUserRoleStore <User>, UsersClient>();
            //services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            //services.AddTransient<IUserEmailStore<User>, UsersClient>();
            //services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            //services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            //services.AddTransient<IUserClaimStore<User>, UsersClient>();
            //services.AddTransient<IUserLoginStore<User>, UsersClient>();

            //services.AddTransient<IRoleStore<Role>, RolesClient>();
            //#endregion

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
            services.AddSignalR();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();

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

            //app.UseMiddleware<TestMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.MapWhen(
                 context => context.Request.Query.ContainsKey("id") && context.Request.Query["id"] == "5",
                 context => context.Run(async request => await request.Response.WriteAsync("Hell id = 5!!!"))
                );

            app.Map("/hhh", context => context.Run(async request => await request.Response.WriteAsync("Hell!!!")));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");

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

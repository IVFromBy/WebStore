using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Domain.Entites.Identity;

namespace WebStore.Clients.Identity
{
    public static class IdentityExtentions
    {
        public static IServiceCollection AddIdentityWebStoreWebAPIClient(this IServiceCollection services)
        {
            services
                .AddTransient<IUserStore<User>, UsersClient>()
                .AddTransient<IUserRoleStore<User>, UsersClient>()
                .AddTransient<IUserPasswordStore<User>, UsersClient>()
                .AddTransient<IUserEmailStore<User>, UsersClient>()
                .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTransient<IUserClaimStore<User>, UsersClient>()
                .AddTransient<IUserLoginStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<Role>, RolesClient>();

            return services;
        }

        public static IdentityBuilder AddIdentityWebStoreWebAPIClient(this IdentityBuilder builder)
        {
            builder.Services.AddIdentityWebStoreWebAPIClient();
            return builder;
        }
    }
}

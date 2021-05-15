using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Entites.DTO.Identity;
using WebStore.Domain.Entites.Identity;
using WebStore.Interfaces;
using WebStore.Interfaces.Services.Identity;

namespace WebStore.Clients.Identity
{
    public class UsersClient : BaseClient, IUsersClient
    {
        public UsersClient(IConfiguration configuration) : base(configuration, WebAPI.Identity.User) { }

        #region Управление пользователями

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/UserId", user, cancel);
            return await response
               .Content
               .ReadAsAsync<string>(cancel)
               .ConfigureAwait(false);
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/UserName", user, cancel);
            return await response
               .Content
               .ReadAsAsync<string>(cancel)
               .ConfigureAwait(false);
        }

        public async Task SetUserNameAsync(User user, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/UserName/{name}", user, cancel);
            user.UserName = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{Addres}/NormalUserName/", user, cancel))
               .Content
               .ReadAsAsync<string>(cancel)
               .ConfigureAwait(false);
        }

        public async Task SetNormalizedUserNameAsync(User user, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/NormalUserName/{name}", user, cancel);
            user.NormalizedUserName = await response.Content.ReadAsAsync<string>(cancel).ConfigureAwait(false);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/User", user, cancel);
            var creation_success = await response
               .Content
               .ReadAsAsync<bool>(cancel)
               .ConfigureAwait(false);

            return creation_success
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancel)
        {
            var response = await PutAsync($"{Addres}/User", user, cancel);
            var update_result = await response
               .Content
               .ReadAsAsync<bool>(cancel).ConfigureAwait(false);

            return update_result
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/User/Delete", user, cancel);
            var delete_result = await response
               .Content
               .ReadAsAsync<bool>(cancel);

            return delete_result
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<User> FindByIdAsync(string id, CancellationToken cancel)
        {
            return await GetAsync<User>($"{Addres}/User/Find/{id}", cancel);
        }

        public async Task<User> FindByNameAsync(string name, CancellationToken cancel)
        {
            return await GetAsync<User>($"{Addres}/User/Normal/{name}", cancel);
        }

        #endregion

        #region Управление ролями

        public async Task AddToRoleAsync(User user, string role, CancellationToken cancel)
        {
            await PostAsync($"{Addres}/Role/{role}", user, cancel);
        }

        public async Task RemoveFromRoleAsync(User user, string role, CancellationToken cancel)
        {
            await PostAsync($"{Addres}/Role/Delete/{role}", user, cancel);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/roles", user, cancel);
            return await response
               .Content
               .ReadAsAsync<IList<string>>(cancel);
        }

        public async Task<bool> IsInRoleAsync(User user, string role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/InRole/{role}", user, cancel);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string role, CancellationToken cancel)
        {
            return await GetAsync<List<User>>($"{Addres}/UsersInRole/{role}", cancel);
        }

        #endregion

        #region Управление hash паролей 

        public async Task SetPasswordHashAsync(User user, string hash, CancellationToken cancel)
        {
            var response = await PostAsync(
                    $"{Addres}/SetPasswordHash", new PasswordHashDto { User = user, Hash = hash },
                    cancel)
               .ConfigureAwait(false);
            user.PasswordHash = await response.Content.ReadAsAsync<string>(cancel).ConfigureAwait(false);
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetPasswordHash", user, cancel);
            return await response
               .Content
               .ReadAsAsync<string>(cancel)
               .ConfigureAwait(false);
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/HasPassword", user, cancel);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel)
               .ConfigureAwait(false);
        }

        #endregion

        #region Управление email

        public async Task SetEmailAsync(User user, string email, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetEmail/{email}", user, cancel);
            user.Email = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetEmail", user, cancel);
            return await response
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetEmailConfirmed", user, cancel);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetEmailConfirmed/{confirmed}", user, cancel);
            user.EmailConfirmed = await response.Content.ReadAsAsync<bool>(cancel);
        }

        public async Task<User> FindByEmailAsync(string email, CancellationToken cancel)
        {
            return await GetAsync<User>($"{Addres}/User/FindByEmail/{email}", cancel);
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/User/GetNormalizedEmail", user, cancel);
            return await response
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task SetNormalizedEmailAsync(User user, string email, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetNormalizedEmail/{email}", user, cancel);
            user.NormalizedEmail = await response.Content.ReadAsAsync<string>(cancel);
        }

        #endregion

        #region Управление телефонными номерами

        public async Task SetPhoneNumberAsync(User user, string phone, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetPhoneNumber/{phone}", user, cancel);
            user.PhoneNumber = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetPhoneNumber", user, cancel);
            return await response
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetPhoneNumberConfirmed", user, cancel);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetPhoneNumberConfirmed/{confirmed}", user, cancel);
            user.PhoneNumberConfirmed = await response.Content.ReadAsAsync<bool>(cancel);
        }

        #endregion

        #region Вход в систему

        public async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancel)
        {
            await PostAsync($"{Addres}/AddLogin", new AddLoginDto { User = user, UserLoginInfo = login }, cancel);
        }

        public async Task RemoveLoginAsync(User user, string LoginProvider, string ProviderKey, CancellationToken cancel)
        {
            await PostAsync($"{Addres}/RemoveLogin/{LoginProvider}/{ProviderKey}", user, cancel);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetLogins", user, cancel);
            return await response
               .Content
               .ReadAsAsync<List<UserLoginInfo>>(cancel);
        }

        public async Task<User> FindByLoginAsync(string LoginProvider, string ProviderKey, CancellationToken cancel)
        {
            return await GetAsync<User>($"{Addres}/User/FindByLogin/{LoginProvider}/{ProviderKey}", cancel);
        }

        #endregion

        #region Блокировки

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetLockoutEndDate", user, cancel);
            return await response
               .Content
               .ReadAsAsync<DateTimeOffset?>(cancel);
        }

        public async Task SetLockoutEndDateAsync(User user, DateTimeOffset? EndDate, CancellationToken cancel)
        {
            var response = await PostAsync(
                $"{Addres}/SetLockoutEndDate",
                new SetLockoutDto { User = user, LockoutEnd = EndDate },
                cancel);
            user.LockoutEnd = await response.Content.ReadAsAsync<DateTimeOffset?>(cancel);
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/IncrementAccessFailedCount", user, cancel);
            return await response
               .Content
               .ReadAsAsync<int>(cancel);
        }

        public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancel)
        {
            await PostAsync($"{Addres}/ResetAccessFailedCont", user, cancel);
        }

        public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{Addres}/GetAccessFailedCount", user, cancel))
               .Content
               .ReadAsAsync<int>(cancel);
        }

        public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetLockoutEnabled", user, cancel);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetLockoutEnabled/{enabled}", user, cancel);
            user.LockoutEnabled = await response.Content.ReadAsAsync<bool>(cancel);
        }

        #endregion

        #region 2х факторка

        public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetTwoFactor/{enabled}", user, cancel);
            user.TwoFactorEnabled = await response.Content.ReadAsAsync<bool>(cancel);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetTwoFactorEnabled", user, cancel);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        #endregion

        #region claims

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetClaims", user, cancel);
            return await response
               .Content
               .ReadAsAsync<List<Claim>>(cancel);
        }

        public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancel)
        {
            await PostAsync(
                $"{Addres}/AddClaims",
                new AddClaimDto { User = user, Claims = claims },
                cancel);
        }

        public async Task ReplaceClaimAsync(User user, Claim OldClaim, Claim NewClaim, CancellationToken cancel)
        {
            await PostAsync(
                $"{Addres}/ReplaceClaim",
                new ReplaceClaimDto { User = user, Claim = OldClaim, NewClaim = NewClaim },
                cancel);
        }

        public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancel)
        {
            await PostAsync(
                $"{Addres}/RemoveClaims",
                new RemoveClaimDto { User = user, Claims = claims },
                cancel);
        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancel)
        {
            return await (await PostAsync($"{Addres}/GetUsersForClaim", claim, cancel))
               .Content
               .ReadAsAsync<List<User>>(cancel);
        }

        #endregion    
    }
}

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entites.DTO.Identity;
using WebStore.Domain.Entites.Identity;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebAPI.Identity.User)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _UserStore;
        public UsersController(WebStoreDB db)
        {
            _UserStore = new UserStore<User, Role, WebStoreDB>(db);
            //_UserStore.AutoSaveChanges = false;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers() => await _UserStore.Users.ToArrayAsync();

        #region Users

        [HttpPost("UserId")]
        public async Task<string> GetUserIdAsync([FromBody] User user) => await _UserStore.GetUserIdAsync(user);

        [HttpPost("UserName")]
        public async Task<string> GetUserNameAsync([FromBody] User user) => await _UserStore.GetUserNameAsync(user);

        [HttpPost("UserName/{name}")]
        public async Task<string> SetUserNameAsync([FromBody] User user, string name)
        {
            await _UserStore.SetUserNameAsync(user, name);
            await _UserStore.UpdateAsync(user);
            return user.UserName;
        }

        [HttpPost("NormaluserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody] User user) => await _UserStore.GetNormalizedUserNameAsync(user);

        [HttpPost("NormaluserName/{name}")]
        public async Task<string> SetNormalizedUserNameAsync([FromBody] User user, string name)
        {
            await _UserStore.SetNormalizedUserNameAsync(user, name);
            await _UserStore.UpdateAsync(user);
            return user.NormalizedUserName;
        }
        [HttpPost("User")]
        public async Task<bool> CreateAsync([FromBody] User user)
        {
            var creatio_result = await _UserStore.CreateAsync(user);
            return creatio_result.Succeeded;
        }

        [HttpPut("User")]
        public async Task<bool> UpdateAsync([FromBody] User user)
        {
            var update_result = await _UserStore.UpdateAsync(user);
            return update_result.Succeeded;
        }

        [HttpPost("User/Delete")]
        public async Task<bool> DeleteAsync([FromBody] User user)
        {
            var delete_result = await _UserStore.DeleteAsync(user);
            return delete_result.Succeeded;
        }


        [HttpGet("User/Find/{id}")]
        public async Task<User> FundByIdAsync(string id) => await _UserStore.FindByIdAsync(id);

        [HttpGet("User/Normal/{name}")]
        public async Task<User> FundByNameAsync(string name) => await _UserStore.FindByNameAsync(name);

        [HttpPost("Role/{role}")]
        public async Task AddToRoleAsync([FromBody] User user, string role, [FromServices] WebStoreDB db)
        {
            await _UserStore.AddToRoleAsync(user, role);
            await db.SaveChangesAsync();
        }

        [HttpPost("Role/Delete/{role}")]
        public async Task RemoveFromRoleAsync([FromBody] User user, string role, [FromServices] WebStoreDB db)
        {
            await _UserStore.RemoveFromRoleAsync(user, role);
            await db.SaveChangesAsync();
        }

        [HttpPost("Roles")]
        public async Task<IList<string>> GetRoleAsync([FromBody] User user) => await _UserStore.GetRolesAsync(user);

        [HttpPost("InRole/{role}")]
        public async Task<bool> IsInRoleAsync([FromBody] User user, string role) => await _UserStore.IsInRoleAsync(user, role);

        [HttpGet("UserInRole/{role}")]
        public async Task<IList<User>> GetUserInRoleAsync(string role) => await _UserStore.GetUsersInRoleAsync(role);

        [HttpPost("GetPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody] User user) => await _UserStore.GetPasswordHashAsync(user);

        [HttpPost("SetPasswordHash")]
        public async Task<string> SetPasswordHashAsync([FromBody] PasswordHashDto hash)
        {
            await _UserStore.SetPasswordHashAsync(hash.User, hash.Hash);
            await _UserStore.UpdateAsync(hash.User);
            return hash.User.PasswordHash;
        }
        [HttpPost("HasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody] User user) => await _UserStore.HasPasswordAsync(user);
        #endregion


        #region Claims

        [HttpPost("GetClaims")]
        public async Task<IList<Claim>> GetClaimsAsync([FromBody] User user) => await _UserStore.GetClaimsAsync(user);

        [HttpPost("AddClaims")]
        public async Task AddClaimsAsync([FromBody] AddClaimDto claimInfo, [FromServices] WebStoreDB db)
        {
            await _UserStore.AddClaimsAsync(claimInfo.User, claimInfo.Claims);
            await db.SaveChangesAsync();
        }

        [HttpPost("RaplaceClaim")]
        public async Task ReplaceClaimAsync([FromBody] ReplaceClaimDto claimInfo, [FromServices] WebStoreDB db)
        {
            await _UserStore.ReplaceClaimAsync(claimInfo.User, claimInfo.Claim, claimInfo.NewClaim);
            await db.SaveChangesAsync();
        }

        [HttpPost("RemoveClaim")]
        public async Task RemoveClaimAsync([FromBody] RemoveClaimDto claimInfo, [FromServices] WebStoreDB db)
        {
            await _UserStore.RemoveClaimsAsync(claimInfo.User, claimInfo.Claims);
            await db.SaveChangesAsync();
        }

        [HttpGet("GetUsersForClaim")]
        public async Task<IList<User>> GetUserForClaimAsync([FromBody] Claim claim) => await _UserStore.GetUsersForClaimAsync(claim);

        #endregion

        #region TwoFactor
        [HttpPost("GetTwoFactorEnable")]
        public async Task<bool> GetTwoFactorEnableAsync([FromBody] User user) => await _UserStore.GetTwoFactorEnabledAsync(user);

        [HttpPost("SetTwoFactor/{enable}")]
        public async Task<bool> SetTwoFactorEnableAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetTwoFactorEnabledAsync(user, enable);
            await _UserStore.UpdateAsync(user);
            return user.TwoFactorEnabled;
        }

        #endregion


        #region Email/Phone

        [HttpPost("GetEmail")]
        public async Task<string> GetEmailAsync([FromBody] User user) => await _UserStore.GetEmailAsync(user);

        [HttpPost("SetEmail/{email}")]
        public async Task<string> SetEmailAsync([FromBody] User user, string email)
        {
            await _UserStore.SetEmailAsync(user, email);
            await _UserStore.UpdateAsync(user);
            return user.Email;
        }

        [HttpPost("GetEmailConfirmed")]
        public async Task<bool> GetEmailConfirmedAsync([FromBody] User user) => await _UserStore.GetEmailConfirmedAsync(user);

        [HttpPost("SetEmailConfirmed/{enable}")]
        public async Task<bool> SetEmailConfirmedAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetEmailConfirmedAsync(user, enable);
            await _UserStore.UpdateAsync(user);
            return user.EmailConfirmed;
        }

        [HttpGet("UserFindByEmail/{email}")]
        public async Task<User> FindByEmailAsync(string email) => await _UserStore.FindByEmailAsync(email);

        [HttpPost("GetNormalizedEmail")]
        public async Task<string> GetNormalizedEmailAsync([FromBody] User user) => await _UserStore.GetNormalizedEmailAsync(user);

        [HttpPost("SetNormalizedEmail/{email?}")]
        public async Task<string> SetNormalizedEmailAsync([FromBody] User user, string email)
        {
            await _UserStore.SetNormalizedEmailAsync(user, email);
            await _UserStore.UpdateAsync(user);
            return user.NormalizedEmail;
        }

        [HttpPost("GetPhoneNumber")]
        public async Task<string> GetPhoneNumberAsync([FromBody] User user) => await _UserStore.GetPhoneNumberAsync(user);

        [HttpPost("SetPhoneNumber/{phone}")]
        public async Task<string> SetPhoneNumberAsync([FromBody] User user, string phone)
        {
            await _UserStore.SetPhoneNumberAsync(user, phone);
            await _UserStore.UpdateAsync(user);
            return user.PhoneNumber;
        }

        [HttpPost("GetPhoneNumberConfirmed")]
        public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody] User user) =>
            await _UserStore.GetPhoneNumberConfirmedAsync(user);

        [HttpPost("SetPhoneNumberConfirmed/{confirmed}")]
        public async Task<bool> SetPhoneNumberConfirmedAsync([FromBody] User user, bool confirmed)
        {
            await _UserStore.SetPhoneNumberConfirmedAsync(user, confirmed);
            await _UserStore.UpdateAsync(user);
            return user.PhoneNumberConfirmed;
        }

        #endregion

        #region Login/Lockout

        [HttpPost("AddLogin")]
        public async Task AddLoginAsync([FromBody] AddLoginDto login, [FromServices] WebStoreDB db)
        {
            await _UserStore.AddLoginAsync(login.User, login.UserLoginInfo);
            await db.SaveChangesAsync();
        }

        [HttpPost("RemoveLogin/{LoginProvider}/{ProviderKey}")]
        public async Task RemoveLoginAsync([FromBody] User user, string LoginProvider, string ProviderKey, [FromServices] WebStoreDB db)
        {
            await _UserStore.RemoveLoginAsync(user, LoginProvider, ProviderKey);
            await db.SaveChangesAsync();
        }

        [HttpPost("GetLogins")]
        public async Task<IList<UserLoginInfo>> GetLoginsAsync([FromBody] User user) => await _UserStore.GetLoginsAsync(user);

        [HttpGet("User/FindByLogin/{LoginProvider}/{ProviderKey}")]
        public async Task<User> FindByLoginAsync(string LoginProvider, string ProviderKey) => await _UserStore.FindByLoginAsync(LoginProvider, ProviderKey);

        [HttpPost("GetLockoutEndDate")]
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync([FromBody] User user) => await _UserStore.GetLockoutEndDateAsync(user);

        [HttpPost("SetLockoutEndDate")]
        public async Task<DateTimeOffset?> SetLockoutEndDateAsync([FromBody] SetLockoutDto LockoutInfo)
        {
            await _UserStore.SetLockoutEndDateAsync(LockoutInfo.User, LockoutInfo.LockoutEnd);
            await _UserStore.UpdateAsync(LockoutInfo.User);
            return LockoutInfo.User.LockoutEnd;
        }

        [HttpPost("IncrementAccessFailedCount")]
        public async Task<int> IncrementAccessFailedCountAsync([FromBody] User user)
        {
            var count = await _UserStore.IncrementAccessFailedCountAsync(user);
            await _UserStore.UpdateAsync(user);
            return count;
        }

        [HttpPost("ResetAccessFailedCount")]
        public async Task<int> ResetAccessFailedCountAsync([FromBody] User user)
        {
            await _UserStore.ResetAccessFailedCountAsync(user);
            await _UserStore.UpdateAsync(user);
            return user.AccessFailedCount;
        }

        [HttpPost("GetAccessFailedCount")]
        public async Task<int> GetAccessFailedCountAsync([FromBody] User user) => await _UserStore.GetAccessFailedCountAsync(user);

        [HttpPost("GetLockoutEnabled")]
        public async Task<bool> GetLockoutEnabledAsync([FromBody] User user) => await _UserStore.GetLockoutEnabledAsync(user);

        [HttpPost("SetLockoutEnabled/{enable}")]
        public async Task<bool> SetLockoutEnabledAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetLockoutEnabledAsync(user, enable);
            await _UserStore.UpdateAsync(user);
            return user.LockoutEnabled;
        }

        #endregion


    }
}

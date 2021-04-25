using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Entites.Identity;
using WebStore.Interfaces;

namespace WebStore.Clients.Identity
{
    public class RolesClient : BaseClient , IRoleStore<Role>
    {
        public RolesClient(IConfiguration configuration) : base(configuration, WebAPI.Identity.Role) { }

        #region IRoleStore<Role>

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync(Addres, role, cancel).ConfigureAwait(false);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancel)
        {
            var response = await PutAsync(Addres, role, cancel).ConfigureAwait(false);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/Delete", role, cancel).ConfigureAwait(false);
            return await response
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetRoleId", role, cancel).ConfigureAwait(false);
            return await response
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetRoleName", role, cancel).ConfigureAwait(false);
            return await response
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task SetRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetRoleName/{name}", role, cancel).ConfigureAwait(false);
            role.Name = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/GetNormalizedRoleName", role, cancel).ConfigureAwait(false);
            return await response
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task SetNormalizedRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{Addres}/SetNormalizedRoleName/{name}", role, cancel).ConfigureAwait(false);
            role.NormalizedName = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<Role> FindByIdAsync(string id, CancellationToken cancel)
        {
            return await GetAsync<Role>($"{Addres}/FindById/{id}", cancel)
               .ConfigureAwait(false);
        }

        public async Task<Role> FindByNameAsync(string name, CancellationToken cancel)
        {
            return await GetAsync<Role>($"{Addres}/FindByName/{name}", cancel)
               .ConfigureAwait(false);
        }

        #endregion

    }
}

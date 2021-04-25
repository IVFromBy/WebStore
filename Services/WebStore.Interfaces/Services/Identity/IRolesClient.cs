using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entites.Identity;

namespace WebStore.Interfaces.Services.Identity
{
    public interface IRolesClient: IRoleStore<Role>
    {

    }
}

using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entites.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "Admin";
        public const string DefaultAdminPassword = "OdminPas1!";

        public string Description { get; set; }
    }
}

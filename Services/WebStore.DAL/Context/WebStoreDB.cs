using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.Identity;
using WebStore.Domain.Entites.Orders;

namespace WebStore.DAL.Context
{
    public class WebStoreDB : IdentityDbContext<User,Role, string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options) { }

    }
}

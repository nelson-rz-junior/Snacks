using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Snacks.Models;

namespace Snacks.Context
{
    public class SnackContext : IdentityDbContext<ApplicationUser>
    {
        public SnackContext(DbContextOptions<SnackContext> options) 
            : base(options)
        {
        }

        public DbSet<Snack> Snacks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VroomParts.Models.Order;
using VroomParts.Models.Product;
using VroomParts.Models.ShoppingCart;
using VroomParts.Models.User;

namespace VroomParts.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<CarPart> CarParts { get; set; }
        public DbSet<Category> Categories { get; set; }  
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarPart>(b =>
            {
                b.HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId);

                b.Property(c => c.Name)
                .IsRequired();
            });
        }
    }
}

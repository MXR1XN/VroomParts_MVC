using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VroomParts.Domain.Cart;
using VroomParts.Domain.Categories;
using VroomParts.Domain.LineItems;
using VroomParts.Domain.Orders;
using VroomParts.Domain.Products;
using VroomParts.Domain.Users;

namespace VroomParts.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<CarPart> CarParts { get; set; }
        public DbSet<Category> Categories { get; set; }  
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
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

            modelBuilder.Entity<CartProduct>(b => 
            {
                b.HasOne(c => c.CarPart)
                .WithMany()
                .HasForeignKey(c => c.CarPartId);

                b.HasKey(c => new { c.CarPartId, c.ApplicationUserId });
            });

            modelBuilder.Entity<ApplicationUser>(b => 
            {
                b.HasMany(au => au.Orders)
                .WithOne()
                .HasForeignKey(o => o.ApplicaionUserId);
            });

            modelBuilder.Entity<Order>(b =>
            {
                b.HasMany(o => o.LineItems)
                .WithOne()
                .HasForeignKey(o => o.OrderId);
            });
        }
    }
}

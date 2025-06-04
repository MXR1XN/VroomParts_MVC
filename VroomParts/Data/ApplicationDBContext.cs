using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VroomParts.Domain.Car;
using VroomParts.Domain.Cart;
using VroomParts.Domain.Categories;
using VroomParts.Domain.LineItems;
using VroomParts.Domain.MissingRecommendations;
using VroomParts.Domain.Orders;
using VroomParts.Domain.Products;
using VroomParts.Domain.TrackViews;
using VroomParts.Domain.Users;
using VroomParts.Domain.VehicleCompatibility;
using VroomParts.Domain.VehicleRecommendations;

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
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleRecommendation> VehicleRecommendations { get; set; }
        public DbSet<VehicleCompatibility> VehicleCompatibility { get; set; }
        public DbSet<ViewedCarPart> ViewedCarParts { get; set; }
        public DbSet<MissingRecommendation> MissingRecommendations { get; set; }

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

            modelBuilder.Entity<ViewedCarPart>(b =>
            {
                b.HasOne(v => v.CarPart)
                .WithMany(v => v.ViewedByUsers)
                .HasForeignKey(c => c.CarPartId);

                b.HasOne(v => v.User)
                .WithMany(v => v.ViewedParts)
                .HasForeignKey(u => u.UserId);

                b.HasKey(v => new { v.CarPartId, v.UserId });
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

            modelBuilder.Entity<Vehicle>(b =>
            {
                b.HasMany(r => r.Recommendations)
                .WithMany(r => r.Recommendations)
                .UsingEntity<VehicleRecommendation>();
            });

            modelBuilder.Entity<Vehicle>(b =>
            {
                b.HasMany(r => r.Compatibility)
                .WithMany(r => r.VehicleCompatibility)
                .UsingEntity<VehicleCompatibility>();
            });

        }
    }
}

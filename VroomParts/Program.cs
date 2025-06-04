using VroomParts.Data;
using Microsoft.EntityFrameworkCore;
using VroomParts.Data.Repository.CarPartRepository;
using VroomParts.Data.Repository.CategoryRepository;
using Microsoft.AspNetCore.Identity;
using VroomParts.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using VroomParts.Data.Repository.ApplicationUserRepository;
using VroomParts.Domain.Users;
using VroomParts.Domain.Products;
using VroomParts.Domain.Categories;
using VroomParts.Domain.Cart;
using VroomParts.Data.Repository.CartRepository;
using VroomParts.Application.Products;
using VroomParts.Application.Categories;
using VroomParts.Application.Cart;
using VroomParts.Application.Orders;
using VroomParts.Data.Repository.OrderRepository;
using VroomParts.Domain.Orders;
using VroomParts.Domain.LineItems;
using VroomParts.Data.Repository.LineItemRepository;
using VroomParts.Application.ApplicationUserService;
using VroomParts.Application.Vehicles;
using VroomParts.Domain.Car;
using VroomParts.Data.Repository.VehicleRepository;
using VroomParts.Application.Recomendations;
using VroomParts.Domain.VehicleRecommendations;
using VroomParts.Data.Repository.RecomendationRepository;
using VroomParts.Domain.TrackViews;
using VroomParts.Data.Repository.TrackViews;
using VroomParts.Domain.MissingRecommendations;
using VroomParts.Data.Repository.MissingRecommendations;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();


        builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => 
            options.SignIn.RequireConfirmedAccount = false).
            AddEntityFrameworkStores<ApplicationDBContext>().
            AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options => {
            options.LoginPath = $"/Identity/Account/Login";
            options.LogoutPath = $"/Identity/Account/Logout";
            options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
        });

        builder.Services.AddRazorPages();

        builder.Services.AddTransient<ICarPartService, CarPartService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<ICartService, CartService>();
		builder.Services.AddTransient<IOrderService, OrderService>();
		builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
        builder.Services.AddTransient<IVehicleService, VehicleService>();
        builder.Services.AddTransient<IRecomendationService, RecomendationService>();

        builder.Services.AddScoped<ICarPartRepository, CarCartRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<ILineItemRepository, LineItemRepository>();
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        builder.Services.AddScoped<IRecomendationRepository, RecomendationRepository>();
        builder.Services.AddScoped<IViewedCarPatrsRepository, ViewedCarPartRepository>();
        builder.Services.AddScoped<IMissingRecommendationRepository, MissingRecomendationRepository>();
        

        builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();


        builder.Services.AddScoped<IEmailSender, EmailSender>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapRazorPages();

        // Areas (Admin, Customer)
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Welcome}/{action=Index}/{id?}"
        );

        // Default 
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Welcome}/{action=Index}/{id?}",
            defaults: new { area = "Customer" }
        );


        app.Run();
    }
}


using VroomParts.Data;
using Microsoft.EntityFrameworkCore;
using VroomParts.Areas.Admin.Application.CarParts;
using VroomParts.Areas.Admin.Application.Categories;
using VroomParts.Data.Repository.CarPartRepository;
using VroomParts.Data.Repository.CategoryRepository;
using Microsoft.AspNetCore.Identity;
using VroomParts.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using VroomParts.Data.Repository.ApplicationUserRepository;
using VroomParts.Data.Repository.ShoppingCartRepository;
using VroomParts.Areas.Admin.Application.ShoppingCartService;
using VroomParts.Models.ShoppingCart;

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
        builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();

        builder.Services.AddScoped<ICarPartRepository, CarCartRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

        builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

        builder.Services.AddScoped<ShoppingCart>();

        builder.Services.AddScoped<IEmailSender, EmailSender>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

        // Default Route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}",
            defaults: new { area = "Customer" } // Force default to Customer area
        );


        app.Run();
    }
}


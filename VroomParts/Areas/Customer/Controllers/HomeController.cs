using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using VroomParts.Application.Cart;
using VroomParts.Application.Categories;
using VroomParts.Application.Products;
using VroomParts.Areas.Customer.ViewModels;
using VroomParts.Models;

namespace VroomParts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ICarPartService _carPartService;
        private readonly ICategoryService _categoryService;
        private readonly ICartService _shoppingCartService;

        public HomeController
            (
            ICarPartService carPartService, 
            ICategoryService categoryService,
            ICartService shoppingCartService
            )
        {
            _shoppingCartService = shoppingCartService;
            _carPartService = carPartService;
            _categoryService = categoryService;
        }

        public IActionResult Index([FromQuery] GetPartsRequest request)
        {
            var carParts = _carPartService.Search(request);

            if (carParts == null)
            {
                return NotFound();
            }

            var modelCarParts = carParts.Select(c => new CartProductViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                VehicleCompatibility = c.VehicleCompatibility,
                ImageUrl = c.ImageUrl,
                Price = c.Price
            }).ToList();

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.SelectedCategories = request.CategoryIds;

            return View(modelCarParts);
        }

        public IActionResult Details(Guid Id)
        {
            var carPart = _carPartService.GetById(Id);


            if (carPart == null)
            {
                return NotFound();
            }

            var model = new CartProductViewModel()
            {
                Id = Id,
                Name = carPart.Name,
                Description = carPart.Description,
                VehicleCompatibility = carPart.VehicleCompatibility,
                ImageUrl = carPart.ImageUrl,
                Price = carPart.Price,
                Quantity = 1
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(Guid Id, int quantity = 1)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            _shoppingCartService.Add(userId, Id, quantity);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using VroomParts.Areas.Admin.Application.CarParts;
using VroomParts.Areas.Admin.Application.Categories;
using VroomParts.Areas.Admin.Application.ShoppingCartService;
using VroomParts.Models;
using VroomParts.Models.ShoppingCart;

namespace VroomParts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ICarPartService _carPartService;
        private readonly ICategoryService _categoryService;
        private readonly IShoppingCartService _shoppingCartService;

        public HomeController
            (
            ICarPartService carPartService, 
            ICategoryService categoryService,
            IShoppingCartService shoppingCartService
            )
        {
            _shoppingCartService = shoppingCartService;
            _carPartService = carPartService;
            _categoryService = categoryService;
        }

        public IActionResult Index([FromQuery] GetPartsRequest request)
        {
            var carParts = _carPartService.FilterCarPartsData(request);

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.SelectedCategories = request.CategoryIds;

            return View(carParts);
        }

        public IActionResult Details(Guid id)
        {
            var cart = _shoppingCartService.GetShoppingCartByPartId(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _shoppingCartService.CreateShoppingCart(shoppingCart, userId);

            return RedirectToAction(nameof(Index));
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

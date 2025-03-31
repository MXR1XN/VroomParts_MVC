using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VroomParts.Areas.Admin.Application.CarParts;
using VroomParts.Areas.Admin.Application.Categories;
using VroomParts.Areas.Admin.Application.ShoppingCartService;
using VroomParts.Data.Repository.ShoppingCartRepository;
using VroomParts.Models;

namespace VroomParts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var shoppingCartVM = _shoppingCartService.GetShoppingCartVM(userId);

            return View(shoppingCartVM);
        }

        public IActionResult Summary() 
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var shoppingCartVM = _shoppingCartService.GetShoppingCartSummaryVM(userId);

            return View(shoppingCartVM);
        }

        /*[HttpPost]
        [ActionName("Summary")]
		public IActionResult SummaryPost(ShoppingCartVM shoppingCartVM)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			shoppingCartVM = _shoppingCartService.GetShoppingCartSummaryVM(userId);

			return View(shoppingCartVM);
		}*/
		public IActionResult Plus(Guid cartId) 
        {
            _shoppingCartService.Plus(cartId);
           return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(Guid cartId)
        {
            _shoppingCartService.Minus(cartId);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(Guid cartId)
        {
            _shoppingCartService.Remove(cartId);
            return RedirectToAction(nameof(Index));
        }

    }
}

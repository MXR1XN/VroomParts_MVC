using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VroomParts.Application.ApplicationUserService;
using VroomParts.Application.Cart;
using VroomParts.Application.Orders;
using VroomParts.Application.Products;
using VroomParts.Areas.Customer.ViewModels;
using VroomParts.Domain.Products;

namespace VroomParts.Areas.Customer.Controllers
{
    [Area("Customer")]
	[Authorize]
	public class ShoppingCartController : Controller
    {
        private readonly ICartService _shoppingCartService;
        private readonly IOrderService _orderService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly ICarPartService _carPartService;

        private const int VIEWEDCOUNT = 2;

		public ShoppingCartController(
            ICartService shoppingCartService, 
            IOrderService orderService,
			IApplicationUserService aplicationUserService,
            ICarPartService carPartService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
            _applicationUserService = aplicationUserService;
            _carPartService = carPartService;

        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var shoppingCartDto = _shoppingCartService.GetCart(userId);

            var viewProducts = _carPartService.GetByViewCount(VIEWEDCOUNT, userId);

            var model = new CartProductsViewModel() 
            {
                Header = shoppingCartDto.Header,
                Products = shoppingCartDto.Products.Select(c => new ProductViewModel() 
                { 
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    Quantity = c.Count,
                    VehicleCompatibility = c.VehicleCompatibilities.Select(v => new VehicleSearchViewModel 
                    {
                        Make = v.Make,
                        Model = v.Model,
                        Year = v.Year
                    }).ToList(),
                    

                }).ToList(),
                ViewedProducts = viewProducts.Select(v => new ProductViewModel 
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    ImageUrl = v.ImageUrl,
                    Price = v.Price,
                    Quantity = 1, 
                    VehicleCompatibility = v.VehicleCompatibilities.Select(vc => new VehicleSearchViewModel
                    {
                        Make = vc.Make,
                        Model = vc.Model,
                        Year = vc.Year
                    }).ToList()
                }).ToList(),
                
                TotalPrice = shoppingCartDto.TotalPrice,    
            };


            return View(model);
        }

        public IActionResult Summary() 
        {
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _shoppingCartService.GetCart(userId);

            var user = _applicationUserService.GetUser(userId);

			var model = new OrderModel
			{
				ApplicaionUserId = userId,
				TotalPrice = cart.TotalPrice,
                StreetAddress = user.StreetAddress,
                PhoneNumber = user.PhoneNumber,
                PostalCode = user.PostalCode,
                City = user.City,
                State = user.State,
            };

			return View(model);
        }

        [HttpPost]
        public IActionResult Summary(OrderModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _orderService.Add(userId, model);
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            TempData["SuccessMessage"] = "Your order was placed successfully!";
            return View();
        }

        public IActionResult Plus(Guid carPartId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.Plus(userId, carPartId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(Guid carPartId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.Minus(userId, carPartId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(Guid carPartId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.Remove(userId, carPartId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveView(Guid carPartId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            _carPartService.RemoveTrackView(userId, carPartId); 

            return RedirectToAction(nameof(Index));
        }

    }
}

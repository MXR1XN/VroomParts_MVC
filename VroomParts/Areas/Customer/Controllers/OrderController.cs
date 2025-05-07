using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VroomParts.Application.Orders;
using VroomParts.Areas.Customer.ViewModels;

namespace VroomParts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var orders = _orderService.GetOrder(userId);

            var model = orders.Select(o => new OrderModel
            {
                Id = o.Id,
                ApplicaionUserId = userId,
                PhoneNumber = o.PhoneNumber,
                City = o.City,
                StreetAddress = o.StreetAddress,
                PostalCode = o.PostalCode,
                State = o.State,
                Products = o.LineItems.Select(l => new LineItemModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description,
                    ImageUrl = l.ImageUrl,
                    OrderId = l.OrderId,
                    Price = l.Price,
                    VehicleCompatibility = l.VehicleCompatibility,
                    Category = l.Category,
                    Quantity = l.Quantity,
                }).ToList(),
                TotalPrice = o.LineItems.Sum(l => l.Price * l.Quantity),
            }).ToList();
            
            return View(model);
        }

        public IActionResult Delete(Guid id) 
        {
            try
            {
                _orderService.Delete(id);
                TempData["SuccessMessage"] = "Order was deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

	}
}

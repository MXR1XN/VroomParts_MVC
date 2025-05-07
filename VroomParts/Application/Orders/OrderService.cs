using Microsoft.EntityFrameworkCore;
using VroomParts.Areas.Customer.ViewModels;
using VroomParts.Domain.Cart;
using VroomParts.Domain.LineItems;
using VroomParts.Domain.Orders;
using VroomParts.Domain.Users;

namespace VroomParts.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IApplicationUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ILineItemRepository _lineItemsRepository;

        public OrderService(
            IOrderRepository orderRepository, 
            IApplicationUserRepository userRepository, 
            ICartRepository cartRepository,
            ILineItemRepository lineItemRepository) 
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _lineItemsRepository = lineItemRepository;
        }

        public void Add(string userId, OrderModel model)
        {
            if (model is null ||
                string.IsNullOrWhiteSpace(model.PhoneNumber) ||
                string.IsNullOrWhiteSpace(model.State) ||
                string.IsNullOrWhiteSpace(model.StreetAddress) ||
                string.IsNullOrWhiteSpace(model.City) ||
                string.IsNullOrWhiteSpace(model.PostalCode))
            {
                return;
            }

            var order = _orderRepository.Create(new Order() 
            {
                Id = Guid.NewGuid(),
                ApplicaionUserId  = userId,
                PhoneNumber = model!.PhoneNumber,
                State = model!.State,
                StreetAddress = model!.StreetAddress,
                City = model!.City,
                PostalCode = model!.PostalCode
            });

            var cartProducts = _cartRepository.Query()
                .Where(c => c.ApplicationUserId == userId)
                .Include(c => c.CarPart)
                    .ThenInclude(c => c!.Category)
                .ToList();

            var lineItems = cartProducts
	            .Select(c => new LineItem()
	            {
		            Id = Guid.NewGuid(),
		            Name = c.CarPart!.Name,
		            Description = c.CarPart.Description,
		            Price = c.CarPart.Price,
		            ImageUrl = c.CarPart.ImageUrl,
                    Category = c.CarPart.Category != null ? c.CarPart.Category.Name : "Unknown",
                    Quantity = c.Count,
                    OrderId = order.Id
				}).ToList();

            _lineItemsRepository.CreateRange(lineItems);

            _cartRepository.DeleteRange(cartProducts); 
		}

        public void Delete(Guid id)
        {
            var entity = _orderRepository.Query()
                .Include(c => c.LineItems)
                .FirstOrDefault(c => c.Id == id);

            if (entity is null)
            {
                throw new Exception("Not found");
            }
            _lineItemsRepository.DeleteRange(entity.LineItems);
            _orderRepository.Delete(entity);
		}

        public List<OrderDto> GetOrder(string userId)
        {


            var orders = _orderRepository.Query()
                .Where(u => u.ApplicaionUserId == userId)
                .Include(l => l.LineItems)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    ApplicaionUserId = userId,
                    PhoneNumber = o.PhoneNumber,
                    City = o.City,
                    StreetAddress = o.StreetAddress,
                    PostalCode = o.PostalCode,
                    State = o.State,
                    LineItems = o.LineItems.Select(c => new LineItemDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Price = c.Price,
                        Description = c.Description,
                        VehicleCompatibility = c.VehicleCompatibility,
                        ImageUrl = c.ImageUrl,
                        Category = c.Category,
                        Quantity = c.Quantity,
                        OrderId = c.OrderId
                    }).ToList(),
                }).ToList();

            return orders;
        }

        public OrderDto GetById(Guid id)
        {
            var order = _orderRepository.Find(id);
            if (order is null) 
            {
                throw new Exception();
            }
            return order.ToDto();
        }
    }
}

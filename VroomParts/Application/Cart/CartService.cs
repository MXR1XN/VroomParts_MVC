using Microsoft.EntityFrameworkCore;
using VroomParts.Domain.Cart;
using VroomParts.Domain.Users;

namespace VroomParts.Application.Cart
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public CartService(ICartRepository cartRepository, IApplicationUserRepository applicationUserRepository)
        {
            _cartRepository = cartRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        public CartDto GetCart(string userId)
        {
            var user = _applicationUserRepository.Query()
                .Include(u => u.CartProducts)
                    .ThenInclude(u => u.CarPart)
                        .ThenInclude(u => u!.Category)
                .FirstOrDefault(u => u.Id == userId);

            if (user is null) 
            {
                return new CartDto() 
                {
                    Header = "Hello, Guest",
                };
            }
            var cart = user.CartProducts.Select(c => c.ToDto()).ToList();   

            return new CartDto() 
            {
                Header = $"Hello, {user.Name}",
                Products = cart,
                TotalPrice = cart.Sum(c => c.Price * c.Count)
            };
        }

        public void Minus(string userId, Guid carPartId)
        {
            var entity = _cartRepository.Query()
            .FirstOrDefault(c => c.ApplicationUserId == userId && c.CarPartId == carPartId);

            if (entity is null)
            {
                return;
            }

            entity.Count--;

            if (entity.Count <= 0)
            {
                _cartRepository.Delete(entity);
                return;
            }

            _cartRepository.Update(entity);
        }

        public void Plus(string userId, Guid carPartId)
        {
            var entity = _cartRepository.Query()
            .FirstOrDefault(c => c.ApplicationUserId == userId && c.CarPartId == carPartId);

            if (entity is null)
            {
                _cartRepository.Create(new CartProduct
                {
                    ApplicationUserId = userId,
                    CarPartId = carPartId,
                    Count = 1
                });

                return;
            }

            entity.Count++;
            _cartRepository.Update(entity);
        }

        public void Remove(string userId, Guid carPartId)
        {
            var entity = _cartRepository.Query()
            .FirstOrDefault(c => c.ApplicationUserId == userId && c.CarPartId == carPartId);

            if (entity is not null)
            {
                _cartRepository.Delete(entity);
            }
        }

        public void Add(string userId, Guid carPartId, int quantity)
        {

            if (string.IsNullOrWhiteSpace(userId) || carPartId == Guid.Empty || quantity <= 0)
            {
                return;
            }

            var entity = _cartRepository.Query()
                .FirstOrDefault(c => c.ApplicationUserId == userId && c.CarPartId == carPartId);

            if (entity is null)
            {
                _cartRepository.Create(new CartProduct
                {
                    ApplicationUserId = userId,
                    CarPartId = carPartId,
                    Count = quantity
                });

                return;

            }
            
            entity.Count += quantity;
            _cartRepository.Update(entity);
        }
    }
}

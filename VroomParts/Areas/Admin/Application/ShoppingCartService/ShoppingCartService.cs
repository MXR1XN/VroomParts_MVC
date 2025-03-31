using VroomParts.Data.Repository.ApplicationUserRepository;
using VroomParts.Data.Repository.CarPartRepository;
using VroomParts.Data.Repository.ShoppingCartRepository;
using VroomParts.Models.ShoppingCart;

namespace VroomParts.Areas.Admin.Application.ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ICarPartRepository _carPartRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ShoppingCartService(
            ICarPartRepository carPartRepository,
            IShoppingCartRepository shoppingCartRepository,
            IApplicationUserRepository applicationUserRepository
            )
        {
            _carPartRepository = carPartRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        public void CreateShoppingCart(ShoppingCart shopping , string userId)
        {
            shopping.ApplicaiotionUserId = userId;

            ShoppingCart? shoppingCartFromDb = _shoppingCartRepository
                .GetAll()
                .FirstOrDefault(u => u.ApplicaiotionUserId == userId && u.PartId == shopping.PartId);

            if (shoppingCartFromDb == null)
            {
                shopping.Id = Guid.NewGuid();
                shopping.Count = 1;
                _shoppingCartRepository.CreateShoppingCart(shopping);
            }
            else 
            {
                shoppingCartFromDb.Count += shopping.Count;
                _shoppingCartRepository.UpdateShoppingCart(shoppingCartFromDb);
            }
        }

        public ShoppingCart GetShoppingCartByPartId(Guid partId)
        {
            var carPart = _carPartRepository.GetById(partId);

            if (carPart == null) return null;


            return new ShoppingCart() { CarPart = carPart, Count = 1, PartId = partId};
        }

		private ShoppingCartVM CreateBaseShoppingCartVM(string? userId) 
        {

            var shoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = _shoppingCartRepository.GetAll().Where(u => u.ApplicaiotionUserId == userId),
                OrderHeader = new()
            };

            foreach (var cart in shoppingCartVM.ShoppingCartList) 
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                shoppingCartVM.OrderHeader.OrderTotal += cart.Price * cart.Count;
            }

            return shoppingCartVM;
        }

		public ShoppingCartVM GetShoppingCartVM(string? userId) => CreateBaseShoppingCartVM(userId);

		public ShoppingCartVM GetShoppingCartSummaryVM(string? userId)
        {

            var shoppingCartVM = GetShoppingCartVM(userId);

			var user = _applicationUserRepository.GetById(userId);

			shoppingCartVM.FillUsersDetails(user);

            return shoppingCartVM;
        }

        public void Plus(Guid cartId)
        {
            var cartFromDb = _shoppingCartRepository.GetById(cartId);
            if (cartFromDb != null)
            {
                cartFromDb.Count +=1;
                _shoppingCartRepository.UpdateShoppingCart(cartFromDb);    
            }
        }
        public void Minus(Guid cartId)
        {
            var cartFromDb = _shoppingCartRepository.GetById(cartId);
            if (cartFromDb != null) 
            {
                if (cartFromDb.Count <= 1)
                {
                    _shoppingCartRepository.DeleteShoppingCart(cartFromDb);
                }
                else 
                {   
                    cartFromDb.Count -= 1;
                    _shoppingCartRepository.UpdateShoppingCart(cartFromDb);
                }
            }
        }
        public void Remove(Guid cartId)
        {
            var cartFromDb = _shoppingCartRepository.GetById(cartId);
            if (cartFromDb != null)
            {
                _shoppingCartRepository.DeleteShoppingCart(cartFromDb);
            }
        }
        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart) 
        {
            if (shoppingCart == null) return 0;

			if (shoppingCart.Count >= 10) return (double) shoppingCart.CarPart.Price * 0.3;
			if (shoppingCart.Count >= 5)  return (double) shoppingCart.CarPart.Price * 0.5;

			return (double) shoppingCart.CarPart.Price;
		}


    }
}

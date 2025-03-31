using System.Net;
using VroomParts.Models.ShoppingCart;

namespace VroomParts.Areas.Admin.Application.ShoppingCartService
{
    public interface IShoppingCartService
    {
        public void CreateShoppingCart(ShoppingCart shopping, string userId);
		public ShoppingCartVM GetShoppingCartVM(string? userId);
		public ShoppingCartVM GetShoppingCartSummaryVM(string userId);
        public ShoppingCart GetShoppingCartByPartId(Guid partId);
        public void Plus(Guid cartId);
        public void Minus(Guid cartId);
        public void Remove(Guid cartId);
    }
}

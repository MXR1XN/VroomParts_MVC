using VroomParts.Models.ShoppingCart;

namespace VroomParts.Data.Repository.ShoppingCartRepository
{
    public interface IShoppingCartRepository
    {
        List<ShoppingCart> GetAll();
        ShoppingCart? GetById(Guid id);
        ShoppingCart CreateShoppingCart(ShoppingCart shopping);
        ShoppingCart UpdateShoppingCart(ShoppingCart shoppingCart);
        ShoppingCart DeleteShoppingCart(ShoppingCart shoppingCart);
    }
}

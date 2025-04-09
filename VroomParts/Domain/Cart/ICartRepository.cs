using VroomParts.Domain.Products;

namespace VroomParts.Domain.Cart
{
    public interface ICartRepository : IRepository<CartProduct>
    {
        List<CarPart> GetUserCart(string userId);
    }
}

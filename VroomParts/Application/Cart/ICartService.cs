using VroomParts.Domain.Cart;

namespace VroomParts.Application.Cart
{
    public interface ICartService
    {
        void Plus(string userId, Guid carPartId);
        void Minus(string userId, Guid carPartId);
        void Remove(string userId, Guid carPartId);
        void Add(string userId, Guid carPartId, int quantity);
        CartDto GetCart(string userId);
    }
}

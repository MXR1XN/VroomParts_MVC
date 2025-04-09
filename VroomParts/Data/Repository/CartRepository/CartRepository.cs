using Microsoft.EntityFrameworkCore;
using VroomParts.Domain.Cart;
using VroomParts.Domain.Products;

namespace VroomParts.Data.Repository.CartRepository
{
    internal sealed class CartRepository : Repository<CartProduct> , ICartRepository
    {
        private readonly ApplicationDBContext _context;

        public CartRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public List<CarPart> GetUserCart(string userId)
        {
            return _context.CartProducts
                .Where(u => u.ApplicationUserId == userId)
                .Include(u => u.CarPart)
                .Select(u => u.CarPart!)
                .ToList();
        }
    }
}

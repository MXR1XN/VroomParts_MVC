using Microsoft.EntityFrameworkCore;
using VroomParts.Models.ShoppingCart;

namespace VroomParts.Data.Repository.ShoppingCartRepository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDBContext _context;

        public ShoppingCartRepository(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public ShoppingCart CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Add(shoppingCart);
            _context.SaveChanges();
            return shoppingCart;
        }

        public ShoppingCart DeleteShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Remove(shoppingCart);
            _context.SaveChanges();
            return shoppingCart;
        }
        public ShoppingCart UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Update(shoppingCart);
            _context.SaveChanges();
            return shoppingCart;
        }

        public List<ShoppingCart> GetAll()
        {
             return _context.ShoppingCarts.Include(c => c.CarPart).ToList();
        }


        public ShoppingCart? GetById(Guid id)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(j => j.Id == id);
            return cart;
        }

    }
}

using VroomParts.Models;
using VroomParts.Models.Order;

namespace VroomParts.Data.Repository.OrderHeaderRepository
{
    public class OrderHeaderRepository : IOrderHeaderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderHeaderRepository(ApplicationDBContext applicationDBContext) 
        {
            _context = applicationDBContext;
        }
        public Order CreateOrderHeader(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order DeleteOrderHeader(Order order)
        {
            _context.Remove(order);
            _context.SaveChanges();
            return order;
        }
        public Order UpdateOrderHeader(Order order)
        {
            _context.Update(order);
            _context.SaveChanges();
            return order;
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order? GetById(Guid id)
        {
            return _context.Orders.FirstOrDefault(x => x.Id == id);
        }

    }
}

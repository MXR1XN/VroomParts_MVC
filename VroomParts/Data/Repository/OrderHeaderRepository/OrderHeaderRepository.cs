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
        public OrderHeader CreateOrderHeader(OrderHeader order)
        {
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        public OrderHeader DeleteOrderHeader(OrderHeader order)
        {
            _context.Remove(order);
            _context.SaveChanges();
            return order;
        }
        public OrderHeader UpdateOrderHeader(OrderHeader order)
        {
            _context.Update(order);
            _context.SaveChanges();
            return order;
        }

        public List<OrderHeader> GetAll()
        {
            return _context.OrderHeaders.ToList();
        }

        public OrderHeader? GetById(Guid id)
        {
            return _context.OrderHeaders.FirstOrDefault(x => x.Id == id);
        }

    }
}

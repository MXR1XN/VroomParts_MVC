using VroomParts.Models.Order;

namespace VroomParts.Data.Repository.OrderDetailRepository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private readonly ApplicationDBContext _context;
        public OrderDetailRepository(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public OrderDetail CreateOrderHeader(OrderDetail orderDetail)
        {
            _context.Add(orderDetail);
            _context.SaveChanges();
            return orderDetail;
        }

        public OrderDetail DeleteOrderHeader(OrderDetail orderDetail)
        {
            _context.Remove(orderDetail);
            _context.SaveChanges();
            return orderDetail;
        }
        public OrderDetail UpdateOrderHeader(OrderDetail orderDetail)
        {
            _context.Update(orderDetail);
            _context.SaveChanges();
            return orderDetail;
        }

        public List<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetail? GetById(Guid id)
        {
           return _context.OrderDetails.FirstOrDefault(x => x.Id == id);
        }

    }
}

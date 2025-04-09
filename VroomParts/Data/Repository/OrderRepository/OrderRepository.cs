using VroomParts.Domain.Orders;

namespace VroomParts.Data.Repository.OrderRepository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext) 
        {
            _context = applicationDBContext;
        }

        public Order? Find(Guid id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);    
        }
    }
}

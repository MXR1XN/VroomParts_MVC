using VroomParts.Models;
using VroomParts.Models.Order;

namespace VroomParts.Data.Repository.OrderHeaderRepository
{
    public interface IOrderHeaderRepository
    {
        List<OrderHeader> GetAll();
        OrderHeader? GetById(Guid id);
        OrderHeader CreateOrderHeader(OrderHeader order);
        OrderHeader UpdateOrderHeader(OrderHeader order);
        OrderHeader DeleteOrderHeader(OrderHeader order);
    }
}

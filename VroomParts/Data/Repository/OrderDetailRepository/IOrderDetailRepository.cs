using VroomParts.Models.Order;

namespace VroomParts.Data.Repository.OrderDetailRepository
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetAll();
        OrderDetail? GetById(Guid id);
        OrderDetail CreateOrderHeader(OrderDetail orderDetail);
        OrderDetail UpdateOrderHeader(OrderDetail orderDetail);
        OrderDetail DeleteOrderHeader(OrderDetail orderDetail);
    }
}

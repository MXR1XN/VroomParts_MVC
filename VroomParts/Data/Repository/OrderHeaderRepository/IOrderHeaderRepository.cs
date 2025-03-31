using VroomParts.Models;
using VroomParts.Models.Order;

namespace VroomParts.Data.Repository.OrderHeaderRepository
{
    public interface IOrderHeaderRepository
    {
        List<Order> GetAll();
        Order? GetById(Guid id);
        Order CreateOrderHeader(Order order);
        Order UpdateOrderHeader(Order order);
        Order DeleteOrderHeader(Order order);
    }
}

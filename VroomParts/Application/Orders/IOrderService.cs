using VroomParts.Areas.Customer.ViewModels;

namespace VroomParts.Application.Orders
{
    public interface IOrderService
    {
        OrderDto GetById(Guid id);
        List<OrderDto> GetOrder(string userId);
        void Add(string userId, OrderModel model);
        void Delete(Guid id);
    }
}

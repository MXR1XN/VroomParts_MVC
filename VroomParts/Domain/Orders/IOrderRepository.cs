using VroomParts.Models;

namespace VroomParts.Domain.Orders
{
    public interface IOrderRepository : IRepository<Order>, IReadByIdRepository<Guid ,Order>
    {

    }
}

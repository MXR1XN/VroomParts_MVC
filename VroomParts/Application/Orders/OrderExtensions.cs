using VroomParts.Domain.Orders;

namespace VroomParts.Application.Orders
{
    public static class OrderExtensions
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                ApplicaionUserId = order.ApplicaionUserId,
                PhoneNumber = order.PhoneNumber,
                State = order.State,
                StreetAddress = order.StreetAddress,
                City = order.City,
                PostalCode = order.PostalCode
            };
        }
    }
}

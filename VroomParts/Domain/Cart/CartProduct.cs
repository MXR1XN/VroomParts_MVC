using VroomParts.Domain.Products;

namespace VroomParts.Domain.Cart
{
    public class CartProduct
    {
        public Guid CarPartId { get; set; }
        public CarPart? CarPart { get; set; }
        public int Count { get; set; }
        public required string ApplicationUserId { get; set; }
    }
}

using VroomParts.Models.Order;

namespace VroomParts.Models.ShoppingCart
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart>? ShoppingCartList { get; set; }
        public Order.Order OrderHeader { get; set; }
    }
}

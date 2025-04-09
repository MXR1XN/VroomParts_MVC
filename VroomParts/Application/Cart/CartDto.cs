namespace VroomParts.Application.Cart
{
    public class CartDto
    {
        public string? Header { get; set; }
        public List<CartProductDTO> Products { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}

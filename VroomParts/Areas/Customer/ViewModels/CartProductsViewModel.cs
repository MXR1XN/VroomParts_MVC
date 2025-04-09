namespace VroomParts.Areas.Customer.ViewModels
{
    public class CartProductsViewModel
    {
        public string? Header { get; set; }
        public List<CartProductViewModel> Products { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}

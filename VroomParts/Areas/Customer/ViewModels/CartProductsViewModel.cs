namespace VroomParts.Areas.Customer.ViewModels
{
    public class CartProductsViewModel
    {
        public string? Header { get; set; }
        public List<ProductViewModel> Products { get; set; } = [];

        public List<ProductViewModel> ViewedProducts { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}

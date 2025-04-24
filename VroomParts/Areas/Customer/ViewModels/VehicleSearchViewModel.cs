namespace VroomParts.Areas.Customer.ViewModels
{
    public class VehicleSearchViewModel
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }

        public List<ProductViewModel> RecommendedProducts { get; set; } = new();
        public List<ProductViewModel> AllProducts { get; set; } = new();
    }
}

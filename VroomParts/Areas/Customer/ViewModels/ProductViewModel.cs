using VroomParts.Areas.Admin.ViewModels;

namespace VroomParts.Areas.Customer.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<VehicleSearchViewModel> VehicleCompatibility { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

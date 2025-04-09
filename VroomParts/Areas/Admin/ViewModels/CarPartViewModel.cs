namespace VroomParts.Areas.Admin.ViewModels
{
    public class CarPartViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; } 
        public string? VehicleCompatibility { get; set; } 
        public string? ImageUrl { get; set; }
        public Guid? CategoryId { get; set; }
    }
}

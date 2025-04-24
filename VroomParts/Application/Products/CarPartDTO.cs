using VroomParts.Application.Vehicles;

namespace VroomParts.Application.Products
{
    public class CarPartDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<VehicleDto> VehicleCompatibilities { get; set; } = [];
        public DateTime DateAdded { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? CategoryId { get; set; }
    }
}

namespace VroomParts.Models.Product
{
    public class CarPart
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string VehicleCompatibility { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

    }

}

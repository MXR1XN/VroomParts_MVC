namespace VroomParts.Areas.Admin.ViewModels
{
    public class CreateCarPartModel
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string VehicleCompatibility { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public Guid? CategoryId { get; set; }
    }
}

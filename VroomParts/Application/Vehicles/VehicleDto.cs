using VroomParts.Application.Products;
using VroomParts.Domain.VehicleRecommendations;

namespace VroomParts.Application.Vehicles
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public List<CarPartDto> RecomendedProducts { get; set; } = new();
    }
}

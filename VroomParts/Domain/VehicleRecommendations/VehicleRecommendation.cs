using VroomParts.Domain.Car;
using VroomParts.Domain.Products;

namespace VroomParts.Domain.VehicleRecommendations
{
    public class VehicleRecommendation
    {
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Guid CarPartId { get; set; }
        public CarPart CarPart { get; set; }
    }
}

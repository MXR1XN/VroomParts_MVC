using VroomParts.Domain.Car;

namespace VroomParts.Application.Vehicles
{
    public static class VehicleExtensions
    {
        public static VehicleDto ToDto(this Vehicle vehicle) 
        {
            return new VehicleDto()
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year
            };
        }
    }
}

using VroomParts.Domain.VehicleRecommendations;

namespace VroomParts.Application.Recomendations
{
    public static class RecomendationExtension
    {
        public static RecomendationDto ToDto(this VehicleRecommendation recomendation) 
        {
            return new RecomendationDto()
            {
                VehicleId = recomendation.VehicleId,
                VehicleMake = recomendation.Vehicle.Make,
                VehicleModel = recomendation.Vehicle.Model,
                VehicleYear = recomendation.Vehicle.Year,
                CarPartId = recomendation.CarPartId,
                CarPartName = recomendation.CarPart.Name,
                CarPartDescription = recomendation.CarPart.Description,
                CarPartPrice = recomendation.CarPart.Price,
                CarPartImageUrl = recomendation.CarPart.ImageUrl
            };
        }
    }
}

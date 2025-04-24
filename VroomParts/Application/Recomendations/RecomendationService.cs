using Microsoft.EntityFrameworkCore;
using VroomParts.Areas.Customer.ViewModels;
using VroomParts.Domain.Car;
using VroomParts.Domain.Products;

namespace VroomParts.Application.Recomendations
{
    public class RecomendationService : IRecomendationService
    {

        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICarPartRepository _carPartRepository;
        public RecomendationService(
            IVehicleRepository vehicleRepository,
            ICarPartRepository carPartRepository
            ) 
        {
            _vehicleRepository = vehicleRepository;
            _carPartRepository = carPartRepository;
        }

        public void AddRecomendation(CreateRecomendationRequest create)
        {
            var car = _vehicleRepository.Find(create.CarId) 
                ?? throw new ArgumentException("Car not found");

            var part = _carPartRepository.Find(create.PartId)
                ?? throw new ArgumentException("Part not found");

            car.Recommendations.Add(part);

            _vehicleRepository.Update(car);
        }

        public void RemoveRecomendation(DeleteRecomendationRequest delete)
        {
            var car = _vehicleRepository.Query()
                .Include(c => c.Recommendations)
                .FirstOrDefault(x => x.Id == delete.CarId)
                ?? throw new ArgumentException("Car not found");

            var part = car.Recommendations.FirstOrDefault(x => x.Id == delete.PartId)
                ?? throw new ArgumentException("Part not found");

            car.Recommendations.Remove(part);

            _vehicleRepository.Update(car);
        }

        public List<RecomendationDto> GetRecomendations()
        {
            return _vehicleRepository.Query()
                .Include(c => c.Recommendations)
                .SelectMany(x => x.Recommendations
                    .Select(c => new RecomendationDto 
                    {
                        CarPartId = c.Id,
                        CarPartDescription = c.Description,
                        CarPartName = c.Name,
                        CarPartImageUrl = c.ImageUrl,
                        CarPartPrice = c.Price,
                        VehicleId = x.Id,
                        VehicleMake = x.Make,
                        VehicleModel = x.Model,
                        VehicleYear = x.Year
                    })
                ).ToList();
        }

        public List<RecomendationDto> GetRecomendationsByModel(SearchRecomendationRequest vehicleSearch)
        {
            var query = _vehicleRepository.Query()
            .Include(c => c.Recommendations).AsQueryable();


            if (!string.IsNullOrWhiteSpace(vehicleSearch.Make))
            {
                query = query.Where(v => v.Make != null && v.Make.Equals(vehicleSearch.Make));
            }

            if (!string.IsNullOrWhiteSpace(vehicleSearch.Model))
            {
                query = query.Where(v => v.Model != null && v.Model.Equals(vehicleSearch.Model));
            }

            if (vehicleSearch.Year.HasValue && vehicleSearch.Year.Value > 0)
            {
                query = query.Where(v => v.Year == vehicleSearch.Year.Value);
            }

           return query
                .SelectMany(x => x.Recommendations
                    .Select(c => new RecomendationDto
                    {
                        CarPartId = c.Id,
                        CarPartDescription = c.Description,
                        CarPartName = c.Name,
                        CarPartImageUrl = c.ImageUrl,
                        CarPartPrice = c.Price,
                        VehicleId = x.Id,
                        VehicleMake = x.Make,
                        VehicleModel = x.Model,
                        VehicleYear = x.Year
                    })
                ).ToList();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using VroomParts.Domain.Car;
using VroomParts.Domain.MissingRecommendations;
using VroomParts.Domain.Products;

namespace VroomParts.Application.Recomendations
{
    public class RecomendationService : IRecomendationService
    {

        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICarPartRepository _carPartRepository;
        private readonly IMissingRecommendationRepository _missingRecommendationRepository;
        public RecomendationService(
            IVehicleRepository vehicleRepository,
            ICarPartRepository carPartRepository,
            IMissingRecommendationRepository missingRecommendationRepository
            )
        {
            _vehicleRepository = vehicleRepository;
            _carPartRepository = carPartRepository;
            _missingRecommendationRepository = missingRecommendationRepository;
        }

        public void AddRecomendation(CreateRecomendationRequest create)
        {
            var car = _vehicleRepository.Find(create.CarId)
                ?? throw new ArgumentException("Car not found");

            var part = _carPartRepository.Find(create.PartId)
                ?? throw new ArgumentException("Part not found");

            car.Recommendations.Add(part);

            if (car != null)
            {
                var missing = _missingRecommendationRepository.Query()
                     .Where(v =>
                     (car.Make == null || v.Make == car.Make) &&
                     (car.Model == null || v.Model == car.Model) &&
                     (!car.Year.HasValue || v.Year == car.Year))
                     .ToList();

                if (missing != null)
                {                  
                    foreach (var item in missing) 
                    {
                        item.IsResolved = true;
                    }
                    _missingRecommendationRepository.UpdateRange(missing);
                }
            }

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
                        VehicleYear = x.Year,
                    })
                ).ToList();
        }


        public List<MissingRecommendationDto> MissingRecommendations() 
        {
            return _missingRecommendationRepository.Query()
                .Where(m => m.IsResolved != true)
                .Select(m => new MissingRecommendationDto 
            {
                Id = m.Id,
                Make = m.Make,
                Model = m.Model,
                Year = m.Year,
                CreatedAt = m.CreatedAt,
                IsResolved = m.IsResolved
            }).ToList();
        }

        public void RemoveMissingRecomendation(Guid missingRecomendationId) 
        {
            var missingRecomendation = _missingRecommendationRepository.Find(missingRecomendationId);
            if (missingRecomendation == null) { return; }   
            _missingRecommendationRepository.Delete(missingRecomendation);
        }

        public List<RecomendationDto> GetPartsByVehicle(SearchRecomendationRequest vehicleSearch)
        {
            var querry = _vehicleRepository.Query()
            .Include(c => c.Recommendations).AsQueryable();


            if (!string.IsNullOrWhiteSpace(vehicleSearch.Make))
            {
                querry = querry.Where(v => v.Make != null && v.Make.Equals(vehicleSearch.Make));
            }

            if (!string.IsNullOrWhiteSpace(vehicleSearch.Model))
            {
                querry = querry.Where(v => v.Model != null && v.Model.Equals(vehicleSearch.Model));
            }

            if (vehicleSearch.Year.HasValue && vehicleSearch.Year.Value > 0)
            {
                querry = querry.Where(v => v.Year == vehicleSearch.Year.Value);
            }


            var recommendations = querry
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

            if (recommendations.Count == 0) 
            {
                var loggedModel = _missingRecommendationRepository.Query()
                .Any(x =>
                    x.Make == vehicleSearch.Make &&
                    x.Model == vehicleSearch.Model &&
                    x.Year == vehicleSearch.Year && 
                    !x.IsResolved
                );
                if (!loggedModel) 
                {                
                    _missingRecommendationRepository.Create(new MissingRecommendation
                    {
                        Make = vehicleSearch.Make,
                        Model = vehicleSearch.Model,
                        Year = vehicleSearch.Year,
                        CreatedAt = DateTime.UtcNow,
                    });
                }
            }

            return recommendations;
        }
    }
}

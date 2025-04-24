using Microsoft.EntityFrameworkCore;
using System.Linq;
using VroomParts.Application.Recomendations;
using VroomParts.Application.Vehicles;
using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Areas.Customer.ViewModels;
using VroomParts.Domain.Car;
using VroomParts.Domain.Products;

namespace VroomParts.Application.Products
{
    public class CarPartService : ICarPartService
    {

        private readonly ICarPartRepository _carPartRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public CarPartService(ICarPartRepository carPartRepository, IVehicleRepository vehicleRepository)
        {
            _carPartRepository = carPartRepository;
            _vehicleRepository = vehicleRepository;
        }

        public CarPartDto Create(CreateCarPartModel model)
        {
            var vehicles = _vehicleRepository.Query()
                .Where(v => model.VehicleIds.Contains(v.Id))
                .ToList();

            var entity = new CarPart()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                DateAdded = DateTime.UtcNow,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                VehicleCompatibility = vehicles
            };

            _carPartRepository.Create(entity);

            return entity.ToDto();
        }

        public CarPartDto Delete(Guid id)
        {
            var entity = _carPartRepository.Find(id);

            if (entity is null) 
            {
                entity = new CarPart();
            }
            _carPartRepository.Delete(entity);
            return entity.ToDto();
        }

        public CarPartDto Edit(Guid id, CarPartViewModel model)
        {
            var vehicles = _vehicleRepository.Query()
                .Where(v => model.VehicleIds.Contains(v.Id))
                .ToList();

            var entity = _carPartRepository.Query()
                .Include(v => v.VehicleCompatibility)
                .FirstOrDefault(p => p.Id == id)
                 ?? throw new ArgumentException("Car part not found.");


            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.DateAdded = DateTime.UtcNow;
            entity.ImageUrl = model.ImageUrl;
            entity.CategoryId = model.CategoryId;


            entity.VehicleCompatibility.Clear();

            foreach (var v in vehicles)
            {
                entity.VehicleCompatibility.Add(v);
            }

            _carPartRepository.Update(entity);
            return entity.ToDto();
        }

        public CarPartDto GetById(Guid id)
        {
            var entity = _carPartRepository.Query()
                .Include(v => v.VehicleCompatibility)
                .FirstOrDefault(p => p.Id == id)
             ?? throw new ArgumentException("Car part not found.");

            return entity.ToDto();
        }

        public List<CarPartDto> GetParts()
        {
            return _carPartRepository.Query()
                .Include(v => v.VehicleCompatibility)
                .Select(p => p.ToDto())
                .ToList();
        }

        public List<CarPartDto> GetByCompatibility(SearchRecomendationRequest compatibilityKey)
        {
            return _vehicleRepository.Query()
                .Where(v =>
                (compatibilityKey.Make == null || v.Make == compatibilityKey.Make) &&
                (compatibilityKey.Model == null || v.Model == compatibilityKey.Model) &&
                (!compatibilityKey.Year.HasValue || v.Year == compatibilityKey.Year))
                .Include(v => v.Compatibility)
                .SelectMany(v => v.Compatibility)
                .Select(p => p.ToDto())
                .Distinct()
                .ToList();
        }

        public List<CarPartDto> Search(GetPartsRequest request)
        {
           
            var parts = string.IsNullOrEmpty(request.PartPartCompatibility)
                ? _carPartRepository.Query()
                : _vehicleRepository.Query()
                    .Where(v => v.Make.Contains(request.PartPartCompatibility) ||
                        v.Model.Contains(request.PartPartCompatibility) ||
                        v.Year.ToString().Contains(request.PartPartCompatibility))
                    .Include(v => v.Compatibility)
                    .SelectMany(v => v.Compatibility).Distinct();


            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                parts = parts.Where(c => c.CategoryId.HasValue && request.CategoryIds.Contains(c.CategoryId.Value));
            }

            if (request.MinPrice != null && request.MinPrice.HasValue)
            {
                parts = parts.Where(c => c.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice != null && request.MaxPrice.HasValue)
            {
                parts = parts.Where(c => c.Price <= request.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(request.SearchPart))
            {
                parts = parts.Where(c => c.Name.Contains(request.SearchPart));
            }

            return parts.Select(c => new CarPartDto
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                CategoryId = c.CategoryId,
                VehicleCompatibilities = c.VehicleCompatibility.Select(vc => new VehicleDto
                {
                    Make = vc.Make,
                    Model = vc.Model,
                    Year = vc.Year
                }).ToList(),
            }).ToList();
        }
    }
}

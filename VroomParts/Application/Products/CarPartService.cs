using Microsoft.EntityFrameworkCore;
using VroomParts.Application.ApplicationUserService;
using VroomParts.Application.Recomendations;
using VroomParts.Application.Vehicles;
using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Domain.Car;
using VroomParts.Domain.Cart;
using VroomParts.Domain.Products;
using VroomParts.Domain.TrackViews;

namespace VroomParts.Application.Products
{
    public class CarPartService : ICarPartService
    {

        private readonly ICarPartRepository _carPartRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IViewedCarPatrsRepository _viewedCarPatrsRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IApplicationUserService _applicationUserService;

        public CarPartService(
            ICarPartRepository carPartRepository, 
            IVehicleRepository vehicleRepository,
            IViewedCarPatrsRepository viewedCarPatrsRepository,
            ICartRepository cartRepository,
            IApplicationUserService applicationUserService
            )
        {
            _carPartRepository = carPartRepository;
            _vehicleRepository = vehicleRepository;
            _viewedCarPatrsRepository = viewedCarPatrsRepository;
            _cartRepository = cartRepository;
            _applicationUserService = applicationUserService;
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
                .Where(v => v.Category != null && v.Category.Name == "Bonus Product")
                .Select(p => p.ToDto())
                .ToList();
        }
        public List<CarPartDto> GetByViewCount(int count, string userId)
        {
            return _viewedCarPatrsRepository.Query()
                .Where(v => v.UserId == userId && v.ViewCount > count)
                .Select(v => v.CarPart!.ToDto())
                .ToList();
        }

        public List<CarPartDto> GetByCompatibility(SearchRecomendationRequest compatibilityKey)
        {
            return _vehicleRepository.Query()
                .Where(v =>
                (compatibilityKey.Make == null || v.Make == compatibilityKey.Make) &&
                (compatibilityKey.Model == null || v.Model == compatibilityKey.Model || v.Model.Contains(compatibilityKey.Model)) &&
                (!compatibilityKey.Year.HasValue || v.Year == compatibilityKey.Year))
                .Include(v => v.Compatibility)
                .SelectMany(v => v.Compatibility)
                .Where(v => v.Category!.Name != "Bonus Product")
                .Select(p => p.ToDto())
                .Distinct()
                .ToList();
        }

        public List<CarPartDto> Search(GetPartsRequest request)
        {

            IQueryable<CarPart> parts;

            if (string.IsNullOrEmpty(request.PartPartCompatibility))
            {
                parts = _carPartRepository.Query();
            }

            else 
            {
                parts = _vehicleRepository.Query()
                    .Where(v => v.Make.Contains(request.PartPartCompatibility) ||
                        v.Model.Contains(request.PartPartCompatibility) ||
                        v.Year.ToString().Contains(request.PartPartCompatibility))
                    .Include(v => v.Compatibility)
                    .SelectMany(v => v.Compatibility).Distinct();
            }

            if (!_applicationUserService.IsAdministrator())
            {
                parts = parts.Where(c => c.Category == null || c.Category.Name != "Bonus Product");
            }

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
                    Year = vc.Year,
                }).ToList(),
            }).ToList();
        }


        public void TrackView(string userId, Guid carPartId)
        {
           var shoppingCart = _cartRepository.Query().Where(s => s.ApplicationUserId == userId)
                .Include(s => s.CarPart)
                .Select(s => s.CarPartId)
                .ToList();

            var viewEntry = _viewedCarPatrsRepository.Query()
                .FirstOrDefault(v => v.UserId == userId && v.CarPartId == carPartId);


            bool isInACart = shoppingCart.Contains(carPartId);

            if (isInACart) 
            {
                if (viewEntry != null) 
                {
                    _viewedCarPatrsRepository.Delete(viewEntry);
                }
                return;
            }

            if (viewEntry != null) 
            {
                viewEntry.ViewCount++;
                _viewedCarPatrsRepository.Update(viewEntry);
            }
            else
            {
                var newEntry = new ViewedCarPart
                {
                    UserId = userId,
                    CarPartId = carPartId,
                    ViewCount = 1
                };
                _viewedCarPatrsRepository.Create(newEntry);
            }
        }

        public void RemoveTrackView(string userId, Guid carPartId)
        {
            var viewEntry = _viewedCarPatrsRepository.Query()
               .FirstOrDefault(v => v.UserId == userId && v.CarPartId == carPartId);

            if (viewEntry != null)
            {
                _viewedCarPatrsRepository.Delete(viewEntry);
            }
            return;

        }
    }
}

/*var parts = string.IsNullOrEmpty(request.PartPartCompatibility)
               ? _carPartRepository.Query()
               : _vehicleRepository.Query()
                   .Where(v => v.Make.Contains(request.PartPartCompatibility) ||
                       v.Model.Contains(request.PartPartCompatibility) ||
                       v.Year.ToString().Contains(request.PartPartCompatibility))
                   .Include(v => v.Compatibility)
                   .SelectMany(v => v.Compatibility)
                   .Where(v => v.Category!.Name != "Bonus Product")
                   .Distinct();*/
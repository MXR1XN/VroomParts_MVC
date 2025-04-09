using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Domain.Products;

namespace VroomParts.Application.Products
{
    public class CarPartService : ICarPartService
    {

        private readonly ICarPartRepository _carPartRepository;

        public CarPartService(ICarPartRepository carPartRepository)
        {
            _carPartRepository = carPartRepository;
        }

        public CarPartDTO Create(CreateCarPartModel model)
        {
            var entity = new CarPart()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                DateAdded = DateTime.UtcNow,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                VehicleCompatibility = model.VehicleCompatibility
            };

            _carPartRepository.Create(entity);

            return entity.ToDto();
        }

        public CarPartDTO Delete(Guid id)
        {
            var entity = _carPartRepository.Find(id);

            if (entity is null) 
            {
                entity = new CarPart();
            }
            _carPartRepository.Delete(entity);
            return entity.ToDto();
        }

        public CarPartDTO Edit(Guid id, CreateCarPartModel model)
        {
            var entity = _carPartRepository.Find(id)
                 ?? throw new ArgumentException("Car part not found.");
            
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.DateAdded = DateTime.UtcNow;
            entity.ImageUrl = model.ImageUrl;
            entity.CategoryId = model.CategoryId;
            entity.VehicleCompatibility = model.VehicleCompatibility;

            _carPartRepository.Update(entity);
            return entity.ToDto();
        }

        public CarPartDTO GetById(Guid id)
        {
            var entity = _carPartRepository.Find(id)
             ?? throw new ArgumentException("Car part not found.");

            return entity.ToDto();
        }

        public List<CarPartDTO> Search(GetPartsRequest request)
        {
            var parts = _carPartRepository.Query();


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

            if (!string.IsNullOrEmpty(request.PartPartCompatibility))
            {
                parts = parts.Where(c => c.VehicleCompatibility.Contains(request.PartPartCompatibility));
            }

            return parts.Select(c => new CarPartDTO
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Description = c.Description,
                VehicleCompatibility = c.VehicleCompatibility,
                ImageUrl = c.ImageUrl,
                CategoryId = c.CategoryId
            }).ToList();
        }
    }
}

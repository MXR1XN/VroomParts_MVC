using System.IO;
using System.Net;
using VroomParts.Data.Repository.CarPartRepository;
using VroomParts.Models.Product;

namespace VroomParts.Areas.Admin.Application.CarParts
{
    public class CarPartService : ICarPartService
    {

        private readonly ICarPartRepository _carPartRepository;

        public CarPartService(ICarPartRepository carPartRepository)
        {
            _carPartRepository = carPartRepository;
        }

        public List<CarPartDTO> GetAllCarParts()
        {
            return _carPartRepository
                .GetAll()
                .Select(EntityToDto)
                .ToList();
        }

        public CarPartDTO GetById(Guid id)
        {
            var carPart = _carPartRepository.GetById(id);
            return carPart != null ? EntityToDto(carPart) : null;
        }

        /*public ShoppingCart ToShoppingCart(Guid id)
        {
            var carPart = _carPartRepository.GetById(id);

            var carPartDTO = EntityToDto(carPart);

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                CarPart = carPartDTO,
                Count = 1,
                PartId = id
            };

            return shoppingCart;
        }*/

        public CarPartDTO CreateCarPart(CarPartDTO carPartDto)
        {
            var carPart = new CarPart()
            {
                Name = carPartDto.Name,
                Price = carPartDto.Price,
                Description = carPartDto.Description,
                VehicleCompatibility = carPartDto.VehicleCompatibility,
                ImageUrl = carPartDto.ImageUrl,
                DateAdded = DateTime.Now,
                CategoryId = carPartDto.CategoryId,
            };

            _carPartRepository.CreateCarPart(carPart);

            return EntityToDto(carPart);
        }
        public CarPartDTO EditCarPart(Guid id, CarPartDTO carPartDto)
        {
            if (id != carPartDto.Id) throw new ArgumentException("Car Part not found");

            var carPart = _carPartRepository.GetById(carPartDto.Id);

            if (carPart == null) throw new ArgumentException("Car Part not found");

            carPart.Name = carPartDto.Name;
            carPart.Price = carPartDto.Price;
            carPart.Description = carPartDto.Description;
            carPart.VehicleCompatibility = carPartDto.VehicleCompatibility;
            carPart.ImageUrl = carPartDto.ImageUrl;
            carPart.CategoryId = carPartDto.CategoryId;

            _carPartRepository.UpdateCarPart(carPart);

            return EntityToDto(carPart);
        }

        public CarPartDTO DeleteCarPart(Guid id)
        {
            var carPart = _carPartRepository.GetById(id);

            if (carPart == null)
            {
                throw new ArgumentException("Car's Part not found");
            }
            _carPartRepository.DeleteCarPart(carPart);

            return EntityToDto(carPart);
        }

        public static CarPartDTO EntityToDto(CarPart carPart)
        {
            if (carPart == null) throw new ArgumentNullException(nameof(carPart));

            return new CarPartDTO()
            {
                Id = carPart.Id,
                Name = carPart.Name,
                Price = carPart.Price,
                Description = carPart.Description,
                VehicleCompatibility = carPart.VehicleCompatibility,
                ImageUrl = carPart.ImageUrl,
                CategoryId = carPart.CategoryId
            };
        }

        public bool CarPartExists(Guid id)
        {
            return _carPartRepository.GetById(id) != null;
        }


        //Give already filtred date instead of filtering upon load
        public List<CarPartDTO> GetList(GetPartsRequest request)
        {
            var parts = _carPartRepository.Query();

            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                parts = parts.Where(c => c.CategoryId.HasValue && request.CategoryIds.Contains(c.CategoryId.Value));
            }


            return parts.Select(c => new CarPartDTO{
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Description = c.Description,
                VehicleCompatibility = c.VehicleCompatibility,
                ImageUrl = c.ImageUrl,
                CategoryId = c.CategoryId
            }).ToList();
        }

        public List<CarPartDTO> FilterCarPartsData(GetPartsRequest request) 
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

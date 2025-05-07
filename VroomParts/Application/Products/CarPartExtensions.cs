using VroomParts.Application.Vehicles;
using VroomParts.Domain.Products;

namespace VroomParts.Application.Products
{
    public static class CarPartExtensions
    {
        public static CarPartDto ToDto(this CarPart carPart)
        { 
            return new CarPartDto() 
            {
                Id = carPart.Id,
                CategoryId = carPart.CategoryId,
                DateAdded = carPart.DateAdded,
                Description = carPart.Description,
                ImageUrl = carPart.ImageUrl,
                Name = carPart.Name,
                Price = carPart.Price,
                VehicleCompatibilities = carPart.VehicleCompatibility.Select(v => new VehicleDto 
                {
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                }).ToList()
            }; 
        }
    }
}

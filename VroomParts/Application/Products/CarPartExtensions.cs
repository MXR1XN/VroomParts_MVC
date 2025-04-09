using VroomParts.Domain.Products;

namespace VroomParts.Application.Products
{
    public static class CarPartExtensions
    {
        public static CarPartDTO ToDto(this CarPart carPart)
        { 
            return new CarPartDTO() 
            {
                Id = carPart.Id,
                CategoryId = carPart.CategoryId,
                DateAdded = carPart.DateAdded,
                Description = carPart.Description,
                ImageUrl = carPart.ImageUrl,
                Name = carPart.Name,
                Price = carPart.Price,
                VehicleCompatibility = carPart.VehicleCompatibility
            }; 
        }
    }
}

using VroomParts.Domain.Categories;

namespace VroomParts.Application.Categories
{
    public static class CategoryExtensions
    {
        public static CategoryDTO ToDto(this Category category) 
        {
            return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}

using VroomParts.Models;

namespace VroomParts.Areas.Admin.Application.Categories
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetAll();
        CategoryDTO GetCategoryById(Guid id);
        CategoryDTO CreateCategory(CategoryDTO categoryDTO);
        CategoryDTO EditCategory(Guid id, CategoryDTO categoryDTO);
        CategoryDTO DeleteCategory(Guid id);

    }
}

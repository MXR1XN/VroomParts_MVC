using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Models;

namespace VroomParts.Application.Categories
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetAll();
        CategoryDTO GetById(Guid id);
        CategoryDTO Create(CreateCategoryModel model);
        CategoryDTO Edit(Guid id, CreateCategoryModel model);
        CategoryDTO Delete(Guid id);

    }
}

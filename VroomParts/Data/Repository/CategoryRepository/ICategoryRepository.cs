using VroomParts.Models.Product;

namespace VroomParts.Data.Repository.CategoryRepository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? GetById(Guid id);
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);
        Category DeleteCategory(Category category);
    }
}

using VroomParts.Data.Repository.CategoryRepository;
using VroomParts.Models.Product;

namespace VroomParts.Areas.Admin.Application.Categories
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryDTO> GetAll()
        {
            return _categoryRepository
                .GetAll()
                .Select(EntityToDTO)
                .ToList();
        }
        public CategoryDTO? GetCategoryById(Guid id)
        {
           var category = _categoryRepository.GetById(id);
            return category != null ? EntityToDTO(category) : null;
        }


        public CategoryDTO CreateCategory(CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.Name,
            };
            _categoryRepository.CreateCategory(category);

            return EntityToDTO(category);
            
        }
        public CategoryDTO EditCategory(Guid id, CategoryDTO categoryDTO)
        {

            if (id != categoryDTO.Id) throw new ArgumentException("Category not found");

            var category = _categoryRepository.GetById(id);

            if (category == null) throw new ArgumentException("Category not found"); 

            categoryDTO.Name = category.Name;

            _categoryRepository.UpdateCategory(category);

            return EntityToDTO(category);

        }

        public CategoryDTO DeleteCategory(Guid id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }
            _categoryRepository.DeleteCategory(category);

            return EntityToDTO(category);
        }

        public static CategoryDTO EntityToDTO(Category category) 
        {
            return new CategoryDTO() 
            { 
                Id = category.Id,
                Name = category.Name 
            };
        }

    }
}

using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Domain.Categories;

namespace VroomParts.Application.Categories
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
            return _categoryRepository.Query().Select(c => c.ToDto()).ToList();
        }

        public CategoryDTO GetById(Guid id)
        {
            var entity = _categoryRepository.Find(id) ?? throw new ArgumentException("Category part not found.");
            return entity.ToDto();
        }


        public CategoryDTO Create(CreateCategoryModel model)
        {
            var enitity = new Category()
            {
                Name = model.Name,
            };
            _categoryRepository.Create(enitity);

            return enitity.ToDto();

        }
        public CategoryDTO Edit(Guid id, CreateCategoryModel model)
        {

            var entity = _categoryRepository.Find(id) ?? throw new ArgumentException("Category part not found.");

            entity.Name = model.Name;

            _categoryRepository.Update(entity);

            return entity.ToDto();

        }

        public CategoryDTO Delete(Guid id)
        {
            var enity = _categoryRepository.Find(id);

            if (enity == null)
            {
                throw new ArgumentException("Category not found");
            }
            _categoryRepository.Delete(enity);

            return enity.ToDto();
        }

    }
}

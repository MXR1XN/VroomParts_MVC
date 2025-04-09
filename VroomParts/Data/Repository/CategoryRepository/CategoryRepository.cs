using VroomParts.Domain.Categories;

namespace VroomParts.Data.Repository.CategoryRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public Category? Find(Guid id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}

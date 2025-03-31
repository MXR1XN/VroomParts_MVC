using VroomParts.Models.Product;

namespace VroomParts.Data.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
        }
        public Category? GetById(Guid id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public Category CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category DeleteCategory(Category category)
        {
            _context.Remove(category);
            _context.SaveChanges();
            return category;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }


        public Category UpdateCategory(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}

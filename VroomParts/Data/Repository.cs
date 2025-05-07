using Microsoft.EntityFrameworkCore;
using VroomParts.Domain;

namespace VroomParts.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _set; 

        private readonly ApplicationDBContext _context;
        public Repository(ApplicationDBContext context) 
        {
            _set = context.Set<T>();
            _context = context;
        }
        public T Create(T entity)
        {
            _set.Add(entity);
            _context.SaveChanges();
            return entity;
        }

		public void CreateRange(IEnumerable<T> entities)
		{
			_set.AddRange(entities);
			_context.SaveChanges();
		}

		public void Delete(T entity)
        {
            _set.Remove(entity);
            _context.SaveChanges();
        }

		public void DeleteRange(IEnumerable<T> entities)
		{
			_set.RemoveRange(entities);
			_context.SaveChanges();
		}

		public IQueryable<T> Query()
        {
            return _set.AsQueryable();
        }

        public T Update(T entity)
        {
            _set.Update(entity);
            _context.SaveChanges();   
            return entity;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _set.UpdateRange(entities);
            _context.SaveChanges();
        }
    }
}

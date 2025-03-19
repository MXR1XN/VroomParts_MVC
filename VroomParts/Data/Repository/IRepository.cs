using System.Linq.Expressions;

namespace VroomParts.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}

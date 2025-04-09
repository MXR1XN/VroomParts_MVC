namespace VroomParts.Domain
{
    public interface ICreateUpdateDeleteRepository<T> where T : class
    {
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);  
    }
}

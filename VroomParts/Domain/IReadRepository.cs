namespace VroomParts.Domain
{
    public interface IReadRepository<T> where T : class
    {
        IQueryable<T> Query();
    }
}

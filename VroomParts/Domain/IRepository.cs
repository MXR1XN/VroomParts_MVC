namespace VroomParts.Domain
{
    public interface IRepository<T> : ICreateUpdateDeleteRepository<T>, IBulkRepository<T> ,IReadRepository<T> where T : class
    {

    }
}

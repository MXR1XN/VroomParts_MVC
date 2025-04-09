namespace VroomParts.Domain
{
    public interface IReadByIdRepository<TKey,T> where T : class
    {
        T? Find(TKey id);
    }
}

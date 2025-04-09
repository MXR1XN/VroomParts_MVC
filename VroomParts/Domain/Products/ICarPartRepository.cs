namespace VroomParts.Domain.Products
{
    public interface ICarPartRepository : IRepository<CarPart>, IReadByIdRepository<Guid, CarPart>
    {
    }
}

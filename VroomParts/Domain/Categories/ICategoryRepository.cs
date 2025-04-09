namespace VroomParts.Domain.Categories
{
    public interface ICategoryRepository : IRepository<Category>, IReadByIdRepository<Guid ,Category>
    {

    }
}

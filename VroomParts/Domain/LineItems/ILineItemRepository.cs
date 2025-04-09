namespace VroomParts.Domain.LineItems
{
	public interface ILineItemRepository : IRepository<LineItem>, IReadByIdRepository<Guid, LineItem>
	{

	}
}

using VroomParts.Domain.LineItems;

namespace VroomParts.Data.Repository.LineItemRepository
{
	public class LineItemRepository : Repository<LineItem>, ILineItemRepository
	{
		private readonly ApplicationDBContext _context;

		public LineItemRepository(ApplicationDBContext applicationDBContext): base(applicationDBContext) 
		{
			_context = applicationDBContext;
		}

		public LineItem? Find(Guid id)
		{
			return _context.LineItems.FirstOrDefault(x => x.Id == id);
		}
	}
}

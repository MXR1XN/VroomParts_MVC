using VroomParts.Domain.Products;
using VroomParts.Domain.Users;

namespace VroomParts.Domain.TrackViews
{
	public class ViewedCarPart
	{
		public Guid CarPartId { get; set; }
		public CarPart? CarPart { get; set; }
		public required string UserId { get; set; }
		public ApplicationUser? User { get; set; }

		public int ViewCount { get; set; }
	}
}

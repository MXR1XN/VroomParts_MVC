namespace VroomParts.Domain.LineItems
{
    public class LineItem
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string Description { get; set; } = string.Empty;
		public string VehicleCompatibility { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }
		public string? Category { get; set; }
		public int Quantity { get; set; }
		public Guid OrderId { get; set; }
	}
}

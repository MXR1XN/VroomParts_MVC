namespace VroomParts.Application.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public required string ApplicaionUserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
		public List<LineItemDto> LineItems { get; set; } = [];
	}
}

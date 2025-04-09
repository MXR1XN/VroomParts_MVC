namespace VroomParts.Application.Cart
{
    public class CartProductDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public string? VehicleCompatibility { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public string? Category { get; set; }
    }
}

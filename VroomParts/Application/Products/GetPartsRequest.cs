namespace VroomParts.Application.Products
{
    public class GetPartsRequest
    {
        public List<Guid>? CategoryIds { get; set; } = [];
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SearchPart { get; set; }
        public string? PartPartCompatibility { get; set; }
    }
}

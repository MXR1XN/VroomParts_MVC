namespace VroomParts.Areas.Admin.Application.CarParts
{
    public class GetPartsRequest
    {
        public List<Guid>? CategoryIds { get; set; } = new List<Guid>();
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SearchPart { get; set; }
        public string? PartPartCompatibility { get; set; }
    }
}

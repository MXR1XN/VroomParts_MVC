namespace VroomParts.Areas.Admin.ViewModels
{
    public class RecomendationViewModel
    {
        public Guid VehicleId { get; set; }
        public string? VehicleMake { get; set; }
        public string? VehicleModel { get; set; }
        public int? VehicleYear { get; set; }

        public Guid CarPartId { get; set; }
        public string CarPartName { get; set; } = string.Empty;
        public decimal CarPartPrice { get; set; }
        public string CarPartDescription { get; set; } = string.Empty;
        public string? CarPartImageUrl { get; set; }
    }
}

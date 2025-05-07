namespace VroomParts.Areas.Admin.ViewModels
{
    public class RecomendationsViewModel
    {
        public Guid SelectedCarId { get; set; }
        public Guid SelectedPartId { get; set; }

        public List<VehicleViewModel>? Cars { get; set; }
        public List<CarPartViewModel>? Parts { get; set; }

        public List<RecomendationViewModel>? Recomendations { get; set; }
        public List<MissingRecomendationViewModel>? MissingRecommendations { get; set; }
    }
}

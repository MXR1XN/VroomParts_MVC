using Microsoft.AspNetCore.Mvc.Rendering;
using VroomParts.Application.Recomendations;

namespace VroomParts.Areas.Admin.ViewModels
{
    public class RecomendationViewModel
    {
        public Guid SelectedCarId { get; set; }
        public Guid SelectedPartId { get; set; }

        public Guid? EditCarId { get; set; }
        public Guid? EditPartId { get; set; }
        public Guid? NewPartId { get; set; }

        public List<SelectListItem> Cars { get; set; }
        public List<SelectListItem> Parts { get; set; }

        public List<RecomendationDto> Recomendations { get; set; } // to DO
    }
}

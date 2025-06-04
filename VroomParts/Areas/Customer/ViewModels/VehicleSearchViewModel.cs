using Microsoft.AspNetCore.Mvc.Rendering;
using VroomParts.Areas.Admin.ViewModels;

namespace VroomParts.Areas.Customer.ViewModels
{
    public class VehicleSearchViewModel
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }

        public List<SelectListItem> Makes { get; set; } = new();
        public List<SelectListItem> Models { get; set; } = new();
        public List<SelectListItem> Years { get; set; } = new();

        public List<ProductViewModel> RecommendedProducts { get; set; } = new();
        public List<ProductViewModel> AllProducts { get; set; } = new();
    }
}

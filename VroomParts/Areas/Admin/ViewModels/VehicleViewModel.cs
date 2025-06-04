using Microsoft.AspNetCore.Mvc.Rendering;

namespace VroomParts.Areas.Admin.ViewModels
{
    public class VehicleViewModel
    {
        public Guid Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
    }
}

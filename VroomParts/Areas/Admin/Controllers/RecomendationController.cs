using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VroomParts.Application.Products;
using VroomParts.Application.Recomendations;
using VroomParts.Application.Vehicles;
using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Utility;

namespace VroomParts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class RecomendationController : Controller
    {
        private readonly IRecomendationService _recomendationService;
        private readonly IVehicleService _vehicleService;
        private readonly ICarPartService _carPartService;

        public RecomendationController(
            IRecomendationService recomendationService,
            IVehicleService vehicleService,
            ICarPartService carPartService
            )
        {
            _recomendationService = recomendationService;
            _vehicleService = vehicleService;
            _carPartService = carPartService;
        }
        public IActionResult Index()
        {
            var model = new RecomendationViewModel
            {
                Cars = _vehicleService.GetVehicles()
                 .Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Model }) 
                 .ToList(),

                Parts = _carPartService.GetParts()
                 .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                 .ToList(),

                Recomendations = _recomendationService.GetRecomendations()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(RecomendationViewModel model)
        {
            _recomendationService.AddRecomendation(new CreateRecomendationRequest
            {
                CarId = model.SelectedCarId,
                PartId = model.SelectedPartId
            });

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(Guid carId, Guid partId)
        {
            _recomendationService.RemoveRecomendation(new DeleteRecomendationRequest
            {
                CarId = carId,
                PartId = partId
            });

            return RedirectToAction("Index");
        }
    }
}

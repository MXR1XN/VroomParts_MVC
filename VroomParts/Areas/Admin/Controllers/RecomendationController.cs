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
            var model = new RecomendationsViewModel
            {
                Cars = _vehicleService.GetVehicles()
                 .Select(v => new VehicleViewModel
                 {
                     Id = v.Id,
                     Model = v.Model,
                     Make = v.Make,
                     Year = v.Year
                 }) 
                 .ToList(),

                Parts = _carPartService.GetParts()
                 .Select(p => new CarPartViewModel 
                 { 
                     Id = p.Id,
                     Name = p.Name
                 })
                 .ToList(),

                Recomendations = _recomendationService.GetRecomendations().Select(r => new RecomendationViewModel 
                {
                    VehicleId = r.VehicleId,
                    VehicleMake = r.VehicleMake,
                    VehicleModel = r.VehicleModel,
                    VehicleYear = r.VehicleYear,
                    CarPartId = r.CarPartId,
                    CarPartName = r.CarPartName,
                    CarPartDescription = r.CarPartDescription,
                    CarPartImageUrl = r.CarPartImageUrl,
                    CarPartPrice = r.CarPartPrice
                }).ToList(),

                MissingRecommendations = _recomendationService.MissingRecommendations().Select(m => new MissingRecomendationViewModel 
                {
                    Id = m.Id,
                    Make = m.Make,
                    Model = m.Model,
                    Year = m.Year,
                    CreatedAt = m.CreatedAt,
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(RecomendationsViewModel model)
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

        public IActionResult MissingRecommendations()
        {
            var list = _recomendationService.MissingRecommendations();
            return View(list);
        }

        public IActionResult RemoveMissingRecommendations(Guid missingRecomendationId)
        {
            _recomendationService.RemoveMissingRecomendation(missingRecomendationId);
            return RedirectToAction("Index");
        }
    }
}

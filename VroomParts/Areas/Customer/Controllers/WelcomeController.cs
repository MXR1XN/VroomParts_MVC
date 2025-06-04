using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VroomParts.Application.Products;
using VroomParts.Application.Recomendations;
using VroomParts.Application.Vehicles;
using VroomParts.Areas.Customer.ViewModels;

namespace VroomParts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class WelcomeController : Controller
    {
        private readonly IRecomendationService _recomendationService;
        private readonly ICarPartService _carPartService;
        private readonly IVehicleService _vehicleService;

        public WelcomeController(
            IRecomendationService recomendationService,
            ICarPartService carPartService,
            IVehicleService vehicleService
            ) 
        {
            _recomendationService = recomendationService;
            _carPartService = carPartService;
            _vehicleService = vehicleService;
        }

        public IActionResult Index(SearchRecomendationRequest searchViewModel)
        {

            if (string.IsNullOrWhiteSpace(searchViewModel.Make) &&
                string.IsNullOrWhiteSpace(searchViewModel.Model) &&
                !searchViewModel.Year.HasValue)
            {
                return base.View(new VehicleSearchViewModel
                {
                    Makes = GetMakes(),
                    Models = GetModels(searchViewModel.Make),
                    Years = GetYears(searchViewModel.Model)
                });
            }

            var filteredProducts = _carPartService.GetByCompatibility(searchViewModel);

            var filteredRecommendations = _recomendationService.GetPartsByVehicle(searchViewModel);

            var model = new VehicleSearchViewModel()
            {
                Make = searchViewModel.Make,
                Model = searchViewModel.Model,
                Year = searchViewModel.Year,

                AllProducts = filteredProducts.Select(c => new ProductViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl
                }).ToList(),

                RecommendedProducts = filteredRecommendations.Select(x => new ProductViewModel()
                {
                    Id = x.CarPartId,
                    Name = x.CarPartName,
                    Description = x.CarPartDescription,
                    Price = x.CarPartPrice,
                    ImageUrl = x.CarPartImageUrl
                }).ToList(),

                Makes = GetMakes(),
                Models = GetModels(searchViewModel.Make),
                Years = GetYears(searchViewModel.Model)
            };

            return View(model);
        }

        private List<SelectListItem> GetMakes()
        {
            return _vehicleService.GetVehicles()
                .Select(v => v.Make)
                .Distinct()
                .Select(make => new SelectListItem { Text = make, Value = make })
                .ToList();
        }

        private List<SelectListItem> GetModels(string? make)
        {
            return _vehicleService.GetVehicles()
                .Where(v => v.Make == make)
                .Select(v => v.Model)
                .Distinct()
                .Select(model => new SelectListItem { Text = model, Value = model })
                .ToList();
        }

        private List<SelectListItem> GetYears(string? model)
        {
            return _vehicleService.GetVehicles()
                .Where(v => v.Model == model)
                .Select(v => v.Year)
                .Distinct()
                .Select(make => new SelectListItem { Text = make.ToString(), Value = make.ToString() })
                .ToList();
        }

    }
}

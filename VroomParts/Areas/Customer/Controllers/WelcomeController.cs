using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VroomParts.Application.Products;
using VroomParts.Application.Recomendations;
using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Areas.Customer.ViewModels;

namespace VroomParts.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class WelcomeController : Controller
    {
        private readonly IRecomendationService _recomendationService;
        private readonly ICarPartService _carPartService;

        public WelcomeController(
            IRecomendationService recomendationService,
            ICarPartService carPartService
            ) 
        {
            _recomendationService = recomendationService;
            _carPartService = carPartService;
        }

        public IActionResult Index(SearchRecomendationRequest searchViewModel)
        {

            if (string.IsNullOrWhiteSpace(searchViewModel.Make) &&
                string.IsNullOrWhiteSpace(searchViewModel.Model) &&
                !searchViewModel.Year.HasValue)
            {
                return base.View(new VehicleSearchViewModel());
            }

            var filteredProducts = _carPartService.GetByCompatibility(searchViewModel);
            var filteredRecommendations = _recomendationService.GetRecomendationsByModel(searchViewModel);

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
                }).ToList()
            };

            return View(model);
        }

    }
}

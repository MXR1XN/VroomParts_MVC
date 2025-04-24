using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VroomParts.Application.Categories;
using VroomParts.Application.Products;
using VroomParts.Application.Vehicles;
using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Utility;

namespace VroomParts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class CarPartController : Controller
    {
        private readonly ICarPartService _carPartService;
        private readonly ICategoryService _categoryService;
        private readonly IVehicleService _vehicleService;

        public CarPartController(ICarPartService carPartService, ICategoryService categoryService, IVehicleService vehicleService)
        {
            _carPartService = carPartService;
            _categoryService = categoryService;
            _vehicleService = vehicleService;
        }

        public IActionResult Index([FromQuery] GetPartsRequest request)
        {

            var carParts = _carPartService.Search(request);

            var categories = _categoryService.GetAll();

            if (carParts == null)
            {
                return NotFound();
            }

            var modelCarParts = carParts.Select(c => new CarPartViewModel() 
            { 
                Id = c.Id, 
                Name = c.Name,
                Price = c.Price,
                VehicleCompatibility = c.VehicleCompatibilities.Select(v => new VehicleViewModel
                {
                    Model = v.Model,
                    Make = v.Make,
                    Year = v.Year
                }).ToList(),
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                CategoryId = c.CategoryId
            }).ToList();

            ViewBag.Categories = categories;
            ViewBag.SelectedCategories = request.CategoryIds;

            return View(modelCarParts);
        }

        public IActionResult Create() 
        {
            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Vehicles = _vehicleService.GetVehicles().Select(v => new VehicleViewModel
            {
                Id = v.Id,
                Model = v.Model,
                Make = v.Make,
                Year = v.Year
            }).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCarPartModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetAll();
                ViewBag.Vehicles = _vehicleService.GetVehicles().Select(v => new VehicleViewModel
                {
                    Id = v.Id,
                    Model = v.Model,
                    Make = v.Make,
                    Year = v.Year
                }).ToList();
                return View(model);
            }
            _carPartService.Create(model);
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Edit(Guid id)
        {
            var carPart = _carPartService.GetById(id);

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Vehicles = _vehicleService.GetVehicles();

            if (carPart == null)
            {
                return NotFound();
            }

            var modelCarPart = new CarPartViewModel()
            {
                Id = carPart.Id,
                Name = carPart.Name,
                Price = carPart.Price,
                Description = carPart.Description,
                ImageUrl = carPart.ImageUrl,
                VehicleCompatibility = carPart.VehicleCompatibilities.Select(v => new VehicleViewModel
                {
                    Model = v.Model,
                    Make = v.Make,
                    Year = v.Year
                }).ToList(),
                CategoryId = carPart.CategoryId
            };

            
            return View(modelCarPart);
        }

        
        [HttpPost]
        public IActionResult Edit(CarPartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetAll();
                ViewBag.Vehicles = _vehicleService.GetVehicles();
                return View(model);
            }

            try
            {
                _carPartService.Edit(model.Id, model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        public IActionResult Delete(Guid id)
        {
            var carPart = _carPartService.GetById(id);

            if (carPart == null)
            {
                return NotFound();
            }

            var modelCarPart = new CarPartViewModel()
            {
                Id = carPart.Id,
                Name = carPart.Name,
                Price = carPart.Price,
                VehicleCompatibility = carPart.VehicleCompatibilities.Select(v => new VehicleViewModel
                {
                    Model = v.Model,
                    Make = v.Make,
                    Year = v.Year,
                    Id = v.Id
                }).ToList(),
                Description = carPart.Description,
                ImageUrl = carPart.ImageUrl,
                CategoryId = carPart.CategoryId
            };

            return View(modelCarPart);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _carPartService.Delete(id);
                TempData["SuccessMessage"] = "Car Part deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

    }
}

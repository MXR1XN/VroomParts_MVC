using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VroomParts.Application.Categories;
using VroomParts.Application.Products;
using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Domain.Categories;
using VroomParts.Domain.Products;
using VroomParts.Utility;

namespace VroomParts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class CarPartController : Controller
    {
        private readonly ICarPartService _carPartService;
        private readonly ICategoryService _categoryService;

        public CarPartController(ICarPartService carPartService, ICategoryService categoryService)
        {
            _carPartService = carPartService;
            _categoryService = categoryService;
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
                VehicleCompatibility = c.VehicleCompatibility,
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCarPartModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetAll();
                return View(model);
            }
            _carPartService.Create(model);
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Edit(Guid id)
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
                VehicleCompatibility = carPart.VehicleCompatibility,
                Description = carPart.Description,
                ImageUrl = carPart.ImageUrl,
                CategoryId = carPart.CategoryId
            };

            ViewBag.Categories = _categoryService.GetAll();
            
            return View(modelCarPart);
        }

        
        [HttpPost]
        public IActionResult Edit(Guid id, CreateCarPartModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _carPartService.Edit(id, model);
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
                VehicleCompatibility = carPart.VehicleCompatibility,
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

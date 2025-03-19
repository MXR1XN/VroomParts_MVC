using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VroomParts.Areas.Admin.Application.CarParts;
using VroomParts.Areas.Admin.Application.Categories;

namespace VroomParts.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarPartController : Controller
    {
        private readonly ICarPartService _carPartService;
        private readonly ICategoryService _categoryService;

        public CarPartController(ICarPartService carPartService, ICategoryService categoryService)
        {
            _carPartService = carPartService;
            _categoryService = categoryService;
        }

        // GET: Displays a list of all car parts in the system.
        public IActionResult Index([FromQuery] GetPartsRequest request)
        {

            var carParts = _carPartService.GetList(request);
            var categories = _categoryService.GetAll();

            ViewBag.Categories = categories;
            ViewBag.SelectedCategories = request.CategoryIds;

            return View(carParts);
        }

        // GET: Displays the form for creating a new car part.
        public IActionResult Create() 
        {
            ViewBag.Categories = _categoryService.GetAll(); 
            return View();
        }

        // POST: Saves a new car part to the database.
        [HttpPost]
        public IActionResult Create(CarPartDTO carPartDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetAll();
                return View(carPartDto);
            }
            _carPartService.CreateCarPart(carPartDto);
            return RedirectToAction(nameof(Index)); 
        }

        // GET: Displays the form to edit an existing car part.
        public IActionResult Edit(Guid id)
        {
            var carPart = _carPartService.GetById(id);

            ViewBag.Categories = _categoryService.GetAll();

            return View(carPart);
        }

        
        // POST: Updates an existing car part in the database.   
        [HttpPost]
        public IActionResult Edit(Guid id, CarPartDTO carPartDto)
        {
            if (!ModelState.IsValid)
            {
                return View(carPartDto);
            }

            try
            {
                _carPartService.EditCarPart(id, carPartDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        // GET: Displays the confirmation page before deleting a car part.
        public IActionResult Delete(Guid id)
        {
            var carPart = _carPartService.GetById(id);
            return carPart != null ? View(carPart) : NotFound();
        }

        // POST: Permanently deletes a car part from the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _carPartService.DeleteCarPart(id);
                TempData["SuccessMessage"] = "Car Part deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        public bool CarPartExists(Guid id)
        {
            return _carPartService.GetById(id) != null;
        }

    }
}

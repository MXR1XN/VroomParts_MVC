using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VroomParts.Areas.Admin.Application.CarParts;
using VroomParts.Areas.Admin.Application.Categories;
using VroomParts.Data;
using VroomParts.Models;

namespace VroomParts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ICarPartService _carPartService;
        private readonly ICategoryService _categoryService;

        public HomeController(ICarPartService carPartService, ICategoryService categoryService)
        {
            _carPartService = carPartService;
            _categoryService = categoryService;
        }

        public IActionResult Index([FromQuery] GetPartsRequest request)
        {
            var carParts = _carPartService.FilterCarPartsData(request);

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.SelectedCategories = request.CategoryIds;

            return View(carParts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

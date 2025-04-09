using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VroomParts.Application.Categories;
using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Domain.Categories;
using VroomParts.Utility;

namespace VroomParts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();

            var model = categories.Select(c => new CategoryViewModel() { Id = c.Id ,Name  = c.Name}).ToList();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _categoryService.Create(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new CategoryViewModel()
            {
                Id = id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, CreateCategoryModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                _categoryService.Edit(id, category);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            var model = new CategoryViewModel()
            {
                Id = id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _categoryService.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}


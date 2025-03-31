using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VroomParts.Areas.Admin.Application;
using VroomParts.Areas.Admin.Application.Categories;
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
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            _categoryService.CreateCategory(categoryDto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            try
            {
                _categoryService.EditCategory(id, categoryDto);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jerseyShoppingCartMvcUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class categoryController : Controller
    {
        private readonly IcategoryRepository _categoryRepo;

        public categoryController(IcategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            var categorys = await _categoryRepo.Getcategorys();
            return View(categorys);
        }

        public IActionResult Addcategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Addcategory(categoryDTO category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }
            try
            {
                var categoryToAdd = new category { categoryName = category.categoryName, Id = category.Id };
                await _categoryRepo.Addcategory(categoryToAdd);
                TempData["successMessage"] = "category added successfully";
                return RedirectToAction(nameof(Addcategory));
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = "category could not added!";
                return View(category);
            }

        }

        public async Task<IActionResult> Updatecategory(int id)
        {
            var category = await _categoryRepo.GetcategoryById(id);
            if (category is null)
                throw new InvalidOperationException($"category with id: {id} does not found");
            var categoryToUpdate = new categoryDTO
            {
                Id = category.Id,
                categoryName = category.categoryName
            };
            return View(categoryToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Updatecategory(categoryDTO categoryToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryToUpdate);
            }
            try
            {
                var category = new category { categoryName = categoryToUpdate.categoryName, Id = categoryToUpdate.Id };
                await _categoryRepo.Updatecategory(category);
                TempData["successMessage"] = "category is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "category could not updated!";
                return View(categoryToUpdate);
            }

        }

        public async Task<IActionResult> Deletecategory(int id)
        {
            var category = await _categoryRepo.GetcategoryById(id);
            if (category is null)
                throw new InvalidOperationException($"category with id: {id} does not found");
            await _categoryRepo.Deletecategory(category);
            return RedirectToAction(nameof(Index));

        }

    }
}

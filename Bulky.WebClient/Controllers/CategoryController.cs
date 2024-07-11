using Bulky.WebClient.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bulky.WebClient.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Categories()
        {
            List<Category> categories = _repository.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Category(int categoryId)
        {
            Category? category = null;
            if (categoryId > 0)
            {
                category = _repository.Get(c => c.CategoryId == categoryId);
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Category(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string message = string.Empty;
                    if (category.CategoryId > 0)
                    {
                        _repository.Update(category);
                        message = "updated";
                    }
                    else
                    {
                        _repository.Add(category);
                        message = "added";
                    }
                    _repository.Save();
                    TempData["success"] = $"Category {message} successfully";
                    return RedirectToAction("Categories");
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException?.Number == 2601)
                    {
                        ModelState.AddModelError("", "Duplicate category found, please enter unique category name");
                    }
                }
            }
            return View();
        }

        public IActionResult Delete(int categoryID)
        {
            Category category = _repository.Get(x => x.CategoryId == categoryID);
            if (category != null)
            {
                _repository.Remove(category);
                _repository.Save();
                TempData["success"] = $"Category removed successfully";
            }
            else
            {
                TempData["error"] = $"Category not found";
            }
            return RedirectToAction("Categories");
        }
    }
}

using Bulky.WebClient.Data;
using Bulky.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bulky.WebClient.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Categories()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Category(int categoryId)
        {
            Category? category = null;
            if (categoryId > 0)
            {
                category = _db.Categories.First(c => c.CategoryId == categoryId);
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
                        _db.Categories.Update(category);
                        message = "updated";
                    }
                    else
                    {
                        _db.Categories.Add(category);
                        message = "added";
                    }
                    _db.SaveChanges();
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
            Category category = _db.Categories.FirstOrDefault(x => x.CategoryId == categoryID);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
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

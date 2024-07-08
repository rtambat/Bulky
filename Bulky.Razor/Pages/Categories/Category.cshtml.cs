using Bulky.Razor.Data;
using Bulky.Razor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Razor.Pages.Categories
{
    public class CategoryModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public CategoryModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int categoryID)
        {
            if (categoryID > 0)
            {
                Category = _db.Categories.First(x => x.CategoryId == categoryID);
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                string message = string.Empty;
                if (Category.CategoryId > 0)
                {
                    _db.Categories.Update(Category);
                    message = "updated";
                }
                else
                {
                    _db.Categories.Add(Category);
                    message = "added";
                }
                _db.SaveChanges();
                TempData["success"] = $"Category {message} successfully";
                return RedirectToPage("Categories");
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;
                if (sqlException?.Number == 2601)
                {
                    ModelState.AddModelError("", "Duplicate category found, please enter unique category name");
                }
                return Page();
            }
        }
    }
}

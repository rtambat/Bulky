using Bulky.Razor.Data;
using Bulky.Razor.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky.Razor.Pages.Categories
{
    public class CategoriesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }

        public CategoriesModel(ApplicationDbContext db)
        {
            _db = db;  
        }

        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}

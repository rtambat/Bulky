using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Bulky.WebClient.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Products()
        {
            List<Product> products = _repository.GetAll().ToList();
            return View(products);
        }

        public IActionResult Product(int ProductId)
        {
            Product? Product = null;
            if (ProductId > 0)
            {
                Product = _repository.Get(c => c.ProductId == ProductId);
            }
            return View(Product);
        }

        [HttpPost]
        public IActionResult Product(Product Product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string message = string.Empty;
                    if (Product.ProductId > 0)
                    {
                        _repository.Update(Product);
                        message = "updated";
                    }
                    else
                    {
                        _repository.Add(Product);
                        message = "added";
                    }
                    _repository.Save();
                    TempData["success"] = $"Product {message} successfully";
                    return RedirectToAction("Products");
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException?.Number == 2601)
                    {
                        ModelState.AddModelError("", "Duplicate product found, please enter unique product name");
                    }
                }
            }
            return View();
        }

        public IActionResult Delete(int ProductID)
        {
            Product Product = _repository.Get(x => x.ProductId == ProductID);
            if (Product != null)
            {
                _repository.Remove(Product);
                _repository.Save();
                TempData["success"] = $"Product removed successfully";
            }
            else
            {
                TempData["error"] = $"Product not found";
            }
            return RedirectToAction("Products");
        }
    }
}

using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;

namespace Bulky.WebClient.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ProductViewModel _model;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _model = new ProductViewModel
            {
                Product = new Product()
            };
        }

        public IActionResult Products()
        {
            List<Product> products = _productRepository.GetAll(nameof(Models.Model.Product.Category)).ToList();
            return View(products);
        }

        public IActionResult Product(int ProductId)
        {
            if (ProductId > 0)
            {
                _model.Product = _productRepository.Get(c => c.ProductId == ProductId);
            }
            IEnumerable<SelectListItem> categories = _categoryRepository.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString(),
            });
            _model.Categories = categories;
            return View(_model);
        }

        [HttpPost]
        public IActionResult Product(ProductViewModel productModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string message = string.Empty;
                    string rootPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\product");
                    if (file != null)
                    {
                        if (!string.IsNullOrWhiteSpace(productModel.Product.ImagePath) &&
                            System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, productModel.Product.ImagePath.TrimStart('\\'))))
                        {
                            System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, productModel.Product.ImagePath.TrimStart('\\')));
                        }
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        using (var fileStream = new FileStream(Path.Combine(rootPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        productModel.Product.ImagePath = $"\\images\\product\\{fileName}";
                    }
                    if (productModel.Product.ProductId > 0)
                    {
                        _productRepository.Update(productModel.Product);
                        message = "updated";
                    }
                    else
                    {
                        _productRepository.Add(productModel.Product);
                        message = "added";
                    }
                    _productRepository.Save();
                    TempData["success"] = $"Product {message} successfully";
                    return RedirectToAction("Products");
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException?.Number == 2601)
                    {
                        ModelState.AddModelError("", "Duplicate product found, please enter unique product details");
                    }
                }
            }
            productModel.Categories = _categoryRepository.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString(),
            });
            return View(productModel);
        }

        public IActionResult Delete(int ProductID)
        {
            Product Product = _productRepository.Get(x => x.ProductId == ProductID);
            if (Product != null)
            {
                _productRepository.Remove(Product);
                _productRepository.Save();
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

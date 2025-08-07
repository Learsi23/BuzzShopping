using System.Diagnostics;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using BuzzShopping.Data;
using BuzzShopping.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuzzShopping.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, AppDbContext context) : BaseController(context)
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IProductService _productService = productService;
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<IActionResult> Index()
        {

            ViewBag.Categories = await _categoryService.GetCategoriesAsync();
            try
            {
               List<ProductDto> featuredProducts = await _productService.GetFeaturedProductsAsync();
                return View(featuredProducts);
            }
            catch (Exception ex) 
            {
               return HandleError(ex);
            }

        }


        public IActionResult DetailProduct(int id)
        {
            var product = _productService.GetProductAsync(id);
            if (product == null)
                return NotFound();

            else return View(product);

        }

        public async Task<IActionResult> Products(int? categoryId, string? search, int page = 1)
        {
            try
            {
                int productsByPage = 12;

                var model = await _productService.GetProductPageAsync(categoryId, search, page, productsByPage);

                ViewBag.Categories = await _categoryService.GetCategoriesAsync();
                if (Request.Headers["X-Request-With"] == "XMLHttpRequest")
                {
                    return PartialView("_ProductsPartial", model);
                }

                return View(model);

            }
            catch (Exception ex) 
            {
                return HandleError(ex);
            }


        }


        public async Task<IActionResult> AddProduct(int id, int amount, int? categoryId, string? search, int page = 1 )
        {
            var cartViewModel = await AddProductToCartAsync(id, amount);
            if (cartViewModel != null)
            {
                return RedirectToAction("Products", new {id, categoryId, search, page});
            }
            else return NotFound();
        }

        public async Task<IActionResult> AddProductIndex(int id, int amount)
        {
            var cartViewModel = await AddProductToCartAsync(id, amount);
            if (cartViewModel != null)
            {
                return RedirectToAction("Index");
            }
            else return NotFound();
        }

        public async Task<IActionResult> AddProductDetails(int id, int amount)
        {
            var cartViewModel = await AddProductToCartAsync(id, amount);
            if (cartViewModel != null)
            {
                return RedirectToAction("DetailProduct", new {id});
            }
            else return NotFound();
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

using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductModel> products = await _productService.FindAll();
            return View(products);
        }
        
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                ProductModel createdProduct = await _productService.Create(product);

                if (createdProduct is not null)
                    return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            ProductModel product = await _productService.FindById(id);

            if(product is not null)
                return View(product);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                ProductModel updatedProduct = await _productService.Update(product);

                if (updatedProduct is not null)
                    return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            ProductModel product = await _productService.FindById(id);

            if(product is not null)
                return View(product);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductModel product)
        {
           
            bool productDeleted = await _productService.Delete(product.Id);

            if (productDeleted)
                return RedirectToAction(nameof(Index));
           
            return View(product);
        }
    }
}

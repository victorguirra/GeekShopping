using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
            IEnumerable<ProductViewModel> products = await _productService.FindAll(string.Empty);
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                string token = await HttpContext.GetTokenAsync("access_token");
                ProductViewModel createdProduct = await _productService.Create(product, token);

                if (createdProduct is not null)
                    return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            ProductViewModel product = await _productService.FindById(id, token);

            if(product is not null)
                return View(product);

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                string token = await HttpContext.GetTokenAsync("access_token");
                ProductViewModel updatedProduct = await _productService.Update(product, token);

                if (updatedProduct is not null)
                    return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            ProductViewModel product = await _productService.FindById(id, token);

            if(product is not null)
                return View(product);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(ProductViewModel product)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            bool productDeleted = await _productService.Delete(product.Id, token);

            if (productDeleted)
                return RedirectToAction(nameof(Index));
           
            return View(product);
        }
    }
}

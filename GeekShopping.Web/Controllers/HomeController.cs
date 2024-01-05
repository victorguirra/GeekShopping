using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(IProductService productService, ICartService cartService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductViewModel> products = await _productService.FindAll(string.Empty);
            return View(products);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            ProductViewModel product = await _productService.FindById(id, token);
            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductViewModel productModel)
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            CartHeaderViewModel cartHeaderViewModel = new CartHeaderViewModel();
            cartHeaderViewModel.UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;

            CartDetailViewModel cartDetailViewModel = new CartDetailViewModel();
            cartDetailViewModel.Count = productModel.Count;
            cartDetailViewModel.ProductId = productModel.Id;
            cartDetailViewModel.Product = await _productService.FindById(productModel.Id, token);

            CartViewModel cartViewModel = new CartViewModel();
            cartViewModel.CartHeader = cartHeaderViewModel;
            cartViewModel.CartDetails = new List<CartDetailViewModel>() { cartDetailViewModel };

            CartViewModel response = await _cartService.AddItemToCart(cartViewModel, token);

            if (response is not null)
                return RedirectToAction(nameof(Index));

            return View(productModel);
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

        [Authorize]
        public async Task<IActionResult> Login()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
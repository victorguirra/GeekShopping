using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            CartViewModel cartViewModel = await FindUserCart();
            return View(cartViewModel);
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel cartViewModel)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            bool couponApplied = await _cartService.ApplyCoupon(cartViewModel, token);

            if (couponApplied)
                return RedirectToAction(nameof(Index));
            
            return View();
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartViewModel model)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            string userId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;

            bool couponRemoved = await _cartService.RemoveCoupon(userId, token);

            if (couponRemoved)
                return RedirectToAction(nameof(Index));

            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            bool removed = await _cartService.RemoveFromCart(id, token);

            if (removed)
                return RedirectToAction(nameof(Index));

            return View();
        }

        private async Task<CartViewModel> FindUserCart()
        {
            string userId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            string token = await HttpContext.GetTokenAsync("access_token");

            CartViewModel cartViewModel = await _cartService.FindCartByUserId(userId, token);

            if (cartViewModel?.CartHeader is not null)
            {
                foreach (CartDetailViewModel detail in cartViewModel.CartDetails)
                    cartViewModel.CartHeader.PurchaseAmount += detail.Product.Price * detail.Count;
            }

            return cartViewModel;
        }
    }
}

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
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
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

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            CartViewModel cartViewModel = await FindUserCart();
            return View(cartViewModel);
        }

        private async Task<CartViewModel> FindUserCart()
        {
            string userId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            string token = await HttpContext.GetTokenAsync("access_token");

            CartViewModel cartViewModel = await _cartService.FindCartByUserId(userId, token);

            if (cartViewModel?.CartHeader is not null)
            {
                string couponCode = cartViewModel.CartHeader.CouponCode;

                if (!string.IsNullOrEmpty(couponCode))
                {
                    CouponViewModel coupon = await _couponService.GetCoupon(couponCode, token);

                    if(coupon?.CouponCode is not null)
                    {
                        cartViewModel.CartHeader.DiscountAmount = coupon.DiscountAmount;
                    }
                }

                foreach (CartDetailViewModel detail in cartViewModel.CartDetails)
                    cartViewModel.CartHeader.PurchaseAmount += detail.Product.Price * detail.Count;

                cartViewModel.CartHeader.PurchaseAmount -= cartViewModel.CartHeader.DiscountAmount;
            }

            return cartViewModel;
        }
    }
}

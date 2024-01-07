using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            CartVO cart = await _repository.FindCartByUserId(id);

            if (cart is null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO cartVO)
        {
            CartVO createdCart = await _repository.SaveOrUpdate(cartVO);

            if (createdCart is null)
                return NotFound();

            return Ok(createdCart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO cartVO)
        {
            CartVO updatedCart = await _repository.SaveOrUpdate(cartVO);

            if (updatedCart is null)
                return NotFound();

            return Ok(updatedCart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<bool>> RemoveCart(int id)
        {
            bool cartRemoved = await _repository.RemoveFromCart(id);

            if (!cartRemoved)
                return BadRequest();

            return Ok(cartRemoved);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO cart)
        {
            string userId = cart.CartHeader.UserId;
            string couponCode = cart.CartHeader.CouponCode;

            bool couponApplied = await _repository.ApplyCoupon(userId, couponCode);

            if (!couponApplied)
                return NotFound();

            return Ok(couponApplied);
        }

        [HttpPost("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(string userId)
        {
            bool couponRemoved = await _repository.RemoveCoupon(userId);

            if (!couponRemoved)
                return NotFound();

            return Ok(couponRemoved);
        }
    }
}
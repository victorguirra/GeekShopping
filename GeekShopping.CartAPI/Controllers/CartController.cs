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
        public async Task<ActionResult<CartVO>> FindById(string userId)
        {
            CartVO cart = await _repository.FindCartByUserId(userId);

            if (cart is null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart/{id}")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO cartVO)
        {
            CartVO createdCart = await _repository.SaveOrUpdate(cartVO);

            if (createdCart is null)
                return NotFound();

            return Ok(createdCart);
        }

        [HttpPut("update-cart/{id}")]
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
    }
}
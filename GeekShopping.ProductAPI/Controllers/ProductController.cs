using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Models;
using GeekShopping.ProductAPI.Repository.Interfaces;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            IEnumerable<ProductVO> products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(Int64 id)
        {
            ProductVO productVO = await _repository.FindById(id);

            if (productVO.Id <= 0)
                return NotFound();

            return Ok(productVO);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO productVO)
        {
            if (productVO is null)
                return BadRequest();

            ProductVO newProductVO = await _repository.Create(productVO);
            return Ok(newProductVO);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO productVO)
        {
            if (productVO is null)
                return BadRequest();

            ProductVO updatedProductVO = await _repository.Update(productVO);
            return Ok(updatedProductVO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(Int64 id)
        {
            bool deleted = await _repository.Delete(id);
            return deleted ? Ok(deleted) : BadRequest();
        }
    }
}

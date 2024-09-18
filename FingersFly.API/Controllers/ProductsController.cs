using FingersFly.Domain.Entities;
using FingersFly.Domain.Interfaces;
using FingersFly.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace FingersFly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepo repo) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery] ProductSpec specs)
        {
            return Ok(await repo.GetProducts(specs));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            return Ok(await repo.GetBrands());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            return Ok(await repo.GetTypes());
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            return Ok(await repo.Add(product));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            return Ok(await repo.GetProductById(Id));
        }

        [HttpPut()]
        public async Task<ActionResult> Update(Product product)
        {
            await repo.Update(product);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            await repo.Delete(Id);
            return Ok();
        }
    }
}

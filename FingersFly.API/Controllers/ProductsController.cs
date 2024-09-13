using FingersFly.API.RequestHelpers;
using FingersFly.Domain.Entities;
using FingersFly.Domain.Interfaces;
using FingersFly.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace FingersFly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery]ProductSpecParams specs)
        {
            var spec = new ProductSpecification(specs);
            var products = await repo.ListWithSpecAsync(spec);

            var count = await repo.CountAsync(spec);
            var pagination = new Pagination<Product>(specs.Index, specs.PageSize, count, products);

            return Ok(pagination);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            return Ok(await repo.ListWithSpecAsync(spec));
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            repo.Add(product);
            return Ok(await repo.SaveAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            return Ok(await repo.GetByIdAsync(Id));
        }

        [HttpPut()]
        public async Task<ActionResult> Update(Product product)
        {
            repo.Update(product);
            return Ok(await repo.SaveAllAsync());
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            repo.Delete(Id);
            return Ok(await repo.SaveAllAsync());
        }
    }
}

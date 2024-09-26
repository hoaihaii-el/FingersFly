using FingersFly.Domain.Entities;
using FingersFly.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FingersFly.API.Controllers
{
    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
        {
            var cart = await cartService.GetCartAsync(id);
            return Ok(cart ?? new ShoppingCart { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            var updatedCart = await cartService.SetCartAsync(cart);
            if (updatedCart == null)
            {
                return BadRequest("Problem with cart");
            }
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string key)
        {
            var result = await cartService.DeleteCartAsync(key);
            if (!result) return BadRequest("Can not delete cart!");
            return Ok();
        }
    }
}

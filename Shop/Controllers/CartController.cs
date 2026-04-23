using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services;
using Shop.Domain.Core;
using Shop.Domain.Core.Exceptions;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/carts/{cartId:guid}/items")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<CartItem>> GetItems(Guid cartId)
        {
            return Ok(_service.GetItems(cartId));
        }

        [HttpPost]
        public ActionResult<int> AddItem(Guid cartId, [FromBody] CartItem item)
        {
            try
            {
                var id = _service.AddItem(cartId, item);
                return CreatedAtAction(nameof(GetItems), new { cartId }, id);
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(
                    new
                    {
                        message = ex.Message,
                        entityType = ex.EntityType,
                        key = ex.Key,
                    }
                );
            }
        }

        [HttpDelete("{itemId:int}")]
        public IActionResult RemoveItem(Guid cartId, int itemId)
        {
            return _service.RemoveItem(cartId, itemId) ? NoContent() : NotFound();
        }
    }
}

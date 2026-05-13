using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services;
using Shop.Domain.Core;
using Shop.Domain.Core.Exceptions;

namespace Shop.Controllers
{
    /// <summary>
    /// Shopping cart endpoints. Carts are identified by a client-supplied string key.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/carts/{cartKey}")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        /// <summary>(v1) Get cart info for the given key.</summary>
        /// <param name="cartKey">Unique cart key.</param>
        /// <returns>The full <see cref="Cart"/> (key + items). If the cart does not exist, an empty cart for that key is returned.</returns>
        /// <response code="200">Cart returned successfully.</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
        public ActionResult<Cart> GetCartV1(string cartKey)
        {
            return Ok(_service.GetCart(cartKey));
        }

        /// <summary>(v2) Get cart items for the given key.</summary>
        /// <param name="cartKey">Unique cart key.</param>
        /// <returns>The list of <see cref="CartItem"/>s in the cart. Empty list if the cart does not exist.</returns>
        /// <response code="200">Items returned successfully.</response>
        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(IReadOnlyList<CartItem>), StatusCodes.Status200OK)]
        public ActionResult<IReadOnlyList<CartItem>> GetItemsV2(string cartKey)
        {
            return Ok(_service.GetItems(cartKey));
        }

        /// <summary>Add an item to the cart. Creates the cart if it doesn't exist yet.</summary>
        /// <param name="cartKey">Unique cart key.</param>
        /// <param name="item">The item to add.</param>
        /// <response code="200">Item was added. The response body is the item id.</response>
        /// <response code="409">An item with the same id already exists in the cart.</response>
        [HttpPost("items")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<int> AddItem(string cartKey, [FromBody] CartItem item)
        {
            try
            {
                var id = _service.AddItem(cartKey, item);
                return Ok(id);
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(new
                {
                    message = ex.Message,
                    entityType = ex.EntityType,
                    key = ex.Key,
                });
            }
        }

        /// <summary>Delete an item from the cart.</summary>
        /// <param name="cartKey">Unique cart key.</param>
        /// <param name="itemId">Identifier of the item to remove.</param>
        /// <response code="200">Item was deleted.</response>
        /// <response code="404">Cart or item not found.</response>
        [HttpDelete("items/{itemId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveItem(string cartKey, int itemId)
        {
            return _service.RemoveItem(cartKey, itemId) ? Ok() : NotFound();
        }
    }
}

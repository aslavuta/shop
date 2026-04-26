using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services;
using Shop.Domain.Core;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<Product>> List()
        {
            return Ok(_service.List());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _service.Get(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public ActionResult<int> Add([FromBody] Product product)
        {
            var id = _service.Add(product);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            product.Id = id;
            return _service.Update(product) ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ? NoContent() : NotFound();
        }
    }
}

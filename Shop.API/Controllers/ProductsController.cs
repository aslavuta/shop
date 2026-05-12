using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Hateoas;
using Shop.Application.Common;
using Shop.Application.Services;
using Shop.Domain.Core;

namespace Shop.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<PagedResult<Product>> List(
            [FromQuery] int? categoryId = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            return Ok(_service.List(categoryId, page, pageSize));
        }

        [HttpGet("{id:int}")]
        public ActionResult<LinkedResource<Product>> Get(int id)
        {
            var product = _service.Get(id);
            if (product is null)
                return NotFound();

            var links = new Dictionary<string, Link>
            {
                ["self"] = new(Url.Action(nameof(Get), new { id })!, "GET"),
                ["category"] = new(Url.Action("Get", "Categories", new { id = product.CategoryId })!, "GET"),
                ["update"] = new(Url.Action(nameof(Update), new { id })!, "PUT"),
                ["delete"] = new(Url.Action(nameof(Delete), new { id })!, "DELETE"),
            };

            return Ok(new LinkedResource<Product> { Data = product, Links = links });
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

using Microsoft.AspNetCore.Mvc;
using Shop.API.Hateoas;
using Shop.Application.Services;
using Shop.Domain.Core;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<Category>> List()
        {
            return Ok(_service.List());
        }

        [HttpGet("{id:int}")]
        public ActionResult<LinkedResource<Category>> Get(int id)
        {
            var category = _service.Get(id);
            if (category is null)
                return NotFound();

            var links = new Dictionary<string, Link>
            {
                ["self"] = new(Url.Action(nameof(Get), new { id })!, "GET"),
                ["products"] = new(Url.Action("List", "Products", new { categoryId = id })!, "GET"),
                ["update"] = new(Url.Action(nameof(Update), new { id })!, "PUT"),
                ["delete"] = new(Url.Action(nameof(Delete), new { id })!, "DELETE"),
            };

            if (category.ParentId is { } parentId)
                links["parent"] = new(Url.Action(nameof(Get), new { id = parentId })!, "GET");

            return Ok(new LinkedResource<Category> { Data = category, Links = links });
        }

        [HttpPost]
        public ActionResult<int> Add([FromBody] Category category)
        {
            var id = _service.Add(category);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            category.Id = id;
            return _service.Update(category) ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ? NoContent() : NotFound();
        }
    }
}

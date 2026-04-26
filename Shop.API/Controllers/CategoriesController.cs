using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<Category> Get(int id)
        {
            var category = _service.Get(id);
            return category is null ? NotFound() : Ok(category);
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

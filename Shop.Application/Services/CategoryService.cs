using Shop.Application.Abstractions;
using Shop.Domain.Core;

namespace Shop.Application.Services;

internal sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public Category? Get(int id) => _repository.GetById(id);

    public IReadOnlyList<Category> List() => _repository.List();

    public int Add(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        return _repository.Add(category);
    }

    public bool Update(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        return _repository.Update(category);
    }

    public bool Delete(int id) => _repository.Delete(id);
}

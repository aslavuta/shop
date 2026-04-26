using Shop.Application.Abstractions;
using Shop.Domain.Core;

namespace Shop.UnitTests.Fakes;

internal sealed class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly Dictionary<int, Category> _store = new();
    private int _nextId = 1;

    public Category? GetById(int id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Category> List() => _store.Values.ToList();

    public int Add(Category category)
    {
        category.Id = _nextId++;
        _store[category.Id] = category;
        return category.Id;
    }

    public bool Update(Category category)
    {
        if (!_store.ContainsKey(category.Id))
            return false;
        _store[category.Id] = category;
        return true;
    }

    public bool Delete(int id) => _store.Remove(id);
}

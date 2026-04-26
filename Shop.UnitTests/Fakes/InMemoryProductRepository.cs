using Shop.Application.Abstractions;
using Shop.Domain.Core;

namespace Shop.UnitTests.Fakes;

internal sealed class InMemoryProductRepository : IProductRepository
{
    private readonly Dictionary<int, Product> _store = new();
    private int _nextId = 1;

    public Product? GetById(int id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Product> List() => _store.Values.ToList();

    public int Add(Product product)
    {
        product.Id = _nextId++;
        _store[product.Id] = product;
        return product.Id;
    }

    public bool Update(Product product)
    {
        if (!_store.ContainsKey(product.Id))
            return false;
        _store[product.Id] = product;
        return true;
    }

    public bool Delete(int id) => _store.Remove(id);
}

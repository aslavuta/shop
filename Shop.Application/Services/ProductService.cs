using Shop.Application.Abstractions;
using Shop.Domain.Core;

namespace Shop.Application.Services;

internal sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Product? Get(int id) => _repository.GetById(id);

    public IReadOnlyList<Product> List() => _repository.List();

    public int Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        return _repository.Add(product);
    }

    public bool Update(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        return _repository.Update(product);
    }

    public bool Delete(int id) => _repository.Delete(id);
}

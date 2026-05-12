using Shop.Application.Abstractions;
using Shop.Application.Common;
using Shop.Domain.Core;

namespace Shop.Application.Services;

internal sealed class ProductService : IProductService
{
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;

    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Product? Get(int id) => _repository.GetById(id);

    public IReadOnlyList<Product> List() => _repository.List();

    public PagedResult<Product> List(int? categoryId, int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = DefaultPageSize;
        if (pageSize > MaxPageSize) pageSize = MaxPageSize;

        return _repository.List(categoryId, page, pageSize);
    }

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

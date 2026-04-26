using Shop.Domain.Core;

namespace Shop.Application.Abstractions;

public interface IProductRepository
{
    Product? GetById(int id);

    IReadOnlyList<Product> List();

    int Add(Product product);

    bool Update(Product product);

    bool Delete(int id);
}

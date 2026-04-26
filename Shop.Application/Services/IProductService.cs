using Shop.Domain.Core;

namespace Shop.Application.Services;

public interface IProductService
{
    Product? Get(int id);

    IReadOnlyList<Product> List();

    int Add(Product product);

    bool Update(Product product);

    bool Delete(int id);
}

using Shop.Application.Common;
using Shop.Domain.Core;

namespace Shop.Application.Services;

public interface IProductService
{
    Product? Get(int id);

    IReadOnlyList<Product> List();

    PagedResult<Product> List(int? categoryId, int page, int pageSize);

    int Add(Product product);

    bool Update(Product product);

    bool Delete(int id);
}

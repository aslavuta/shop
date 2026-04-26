using Shop.Domain.Core;

namespace Shop.Application.Abstractions;

public interface ICategoryRepository
{
    Category? GetById(int id);

    IReadOnlyList<Category> List();

    int Add(Category category);

    bool Update(Category category);

    bool Delete(int id);
}

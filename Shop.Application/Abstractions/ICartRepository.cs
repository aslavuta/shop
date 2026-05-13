using Shop.Domain.Core;

namespace Shop.Application.Abstractions;

public interface ICartRepository
{
    Cart? GetById(string id);

    void Save(Cart cart);
}

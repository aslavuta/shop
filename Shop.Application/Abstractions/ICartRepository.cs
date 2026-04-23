using Shop.Domain.Core;

namespace Shop.Application.Abstractions;

public interface ICartRepository
{
    Cart? GetById(Guid id);

    void Save(Cart cart);
}

using Shop.Domain.Core;

namespace Shop.Application.Services;

public interface ICartService
{
    IReadOnlyList<CartItem> GetItems(Guid cartId);

    int AddItem(Guid cartId, CartItem item);

    bool RemoveItem(Guid cartId, int itemId);
}

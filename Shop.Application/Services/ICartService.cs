using Shop.Domain.Core;

namespace Shop.Application.Services;

public interface ICartService
{
    Cart GetCart(string cartKey);

    IReadOnlyList<CartItem> GetItems(string cartKey);

    int AddItem(string cartKey, CartItem item);

    bool RemoveItem(string cartKey, int itemId);
}

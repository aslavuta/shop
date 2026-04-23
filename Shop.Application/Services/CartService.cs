using Shop.Application.Abstractions;
using Shop.Domain.Core;

namespace Shop.Application.Services;

internal sealed class CartService : ICartService
{
    private readonly ICartRepository _repository;

    public CartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<CartItem> GetItems(Guid cartId)
    {
        var cart = _repository.GetById(cartId);
        return cart?.Items.ToList() ?? [];
    }

    public int AddItem(Guid cartId, CartItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var cart = _repository.GetById(cartId) ?? new Cart { Id = cartId };
        cart.AddItem(item);
        _repository.Save(cart);

        return item.Id;
    }

    public bool RemoveItem(Guid cartId, int itemId)
    {
        var cart = _repository.GetById(cartId);
        if (cart is null)
        {
            return false;
        }

        var removed = cart.RemoveItem(itemId);
        if (removed)
        {
            _repository.Save(cart);
        }

        return removed;
    }
}

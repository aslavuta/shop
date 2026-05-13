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

    public Cart GetCart(string cartKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cartKey);
        return _repository.GetById(cartKey) ?? new Cart { Id = cartKey };
    }

    public IReadOnlyList<CartItem> GetItems(string cartKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cartKey);
        var cart = _repository.GetById(cartKey);
        return cart?.Items.ToList() ?? [];
    }

    public int AddItem(string cartKey, CartItem item)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cartKey);
        ArgumentNullException.ThrowIfNull(item);

        var cart = _repository.GetById(cartKey) ?? new Cart { Id = cartKey };
        cart.AddItem(item);
        _repository.Save(cart);

        return item.Id;
    }

    public bool RemoveItem(string cartKey, int itemId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cartKey);

        var cart = _repository.GetById(cartKey);
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

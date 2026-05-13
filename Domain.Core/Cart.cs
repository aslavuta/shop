using Shop.Domain.Core.Exceptions;

namespace Shop.Domain.Core
{
    /// <summary>
    /// A shopping cart identified by a client-supplied string key, containing a collection of <see cref="CartItem"/>s.
    /// </summary>
    public class Cart
    {
        /// <summary>Unique cart key supplied by the caller (e.g. a session/customer identifier).</summary>
        public required string Id { get; init; }

        /// <summary>Items currently in the cart.</summary>
        public List<CartItem> Items { get; init; } = new();

        /// <summary>Adds an item to the cart. Throws <see cref="DuplicateEntityException"/> when an item with the same Id is already present.</summary>
        public void AddItem(CartItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var existing = Items.FirstOrDefault(i => i.Id == item.Id);
            existing.EnsureNotDuplicate();

            Items.Add(item);
        }

        /// <summary>Removes the item with the given id. Returns <c>true</c> when an item was removed; <c>false</c> when no such item existed.</summary>
        public bool RemoveItem(int itemId)
        {
            var existing = Items.FirstOrDefault(i => i.Id == itemId);
            if (existing is null)
            {
                return false;
            }

            Items.Remove(existing);
            return true;
        }
    }

    public record Image(string Url, string Text);

    public static class CartItemExtensions
    {
        public static void EnsureNotDuplicate(this CartItem? existing)
        {
            if (existing is not null)
            {
                throw new DuplicateEntityException(nameof(CartItem), existing.Id);
            }
        }
    }
}

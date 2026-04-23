using Shop.Domain.Core.Exceptions;

namespace Shop.Domain.Core
{
    public class Cart
    {
        public required Guid Id { get; init; }

        public List<CartItem> Items { get; init; } = new();

        public void AddItem(CartItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var existing = Items.FirstOrDefault(i => i.Id == item.Id);
            existing.EnsureNotDuplicate();

            Items.Add(item);
        }

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

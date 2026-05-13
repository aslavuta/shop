namespace Shop.Domain.Core
{
    /// <summary>An item placed in a <see cref="Cart"/>.</summary>
    public class CartItem
    {
        /// <summary>Item identifier (unique within a cart).</summary>
        public required int Id { get; init; }

        /// <summary>Display name of the item.</summary>
        public required string Name { get; init; }

        /// <summary>Optional image associated with the item.</summary>
        public Image? Image { get; init; }

        /// <summary>Unit price.</summary>
        public required decimal Price { get; init; }

        /// <summary>Quantity ordered.</summary>
        public int Quantity { get; init; }
    }
}

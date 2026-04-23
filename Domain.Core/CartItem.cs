namespace Shop.Domain.Core
{
    public class CartItem
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public Image? Image { get; init; }
        public required decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}

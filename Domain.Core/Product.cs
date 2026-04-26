namespace Shop.Domain.Core
{
    public class Product
    {
        public const int NameMaxLength = 50;

        private string _name = null!;

        public int Id { get; set; }

        public required string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Product name is required.", nameof(Name));
                if (value.Length > NameMaxLength)
                    throw new ArgumentException($"Product name must be at most {NameMaxLength} characters.", nameof(Name));
                _name = value;
            }
        }

        public string? Description { get; set; }
        public string? Image { get; set; }
        public required int CategoryId { get; set; }
        public Category? Category { get; set; }

        private decimal _price;
        public required decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Price), value, "Product price must be greater than or equal to zero.");
                _price = value;
            }
        }

        private int _amount;
        public required int Amount
        {
            get => _amount;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Amount), value, "Product amount must be a positive integer.");
                _amount = value;
            }
        }
    }
}

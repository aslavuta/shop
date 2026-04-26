namespace Shop.Domain.Core
{
    public class Category
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
                    throw new ArgumentException("Category name is required.", nameof(Name));
                if (value.Length > NameMaxLength)
                    throw new ArgumentException($"Category name must be at most {NameMaxLength} characters.", nameof(Name));
                _name = value;
            }
        }

        public string? Image { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
    }
}

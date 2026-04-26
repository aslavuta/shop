using Shop.Domain.Core;

namespace Shop.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void Price_WhenNegative_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product
            {
                Name = "Widget",
                CategoryId = 1,
                Price = -0.01m,
                Amount = 1,
            });
        }

        [Fact]
        public void Price_WhenZero_IsAllowed()
        {
            var product = new Product
            {
                Name = "Free Sample",
                CategoryId = 1,
                Price = 0m,
                Amount = 1,
            };

            Assert.Equal(0m, product.Price);
        }

        [Fact]
        public void Amount_WhenZero_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product
            {
                Name = "Widget",
                CategoryId = 1,
                Price = 1m,
                Amount = 0,
            });
        }

        [Fact]
        public void Amount_WhenNegative_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product
            {
                Name = "Widget",
                CategoryId = 1,
                Price = 1m,
                Amount = -5,
            });
        }
    }
}

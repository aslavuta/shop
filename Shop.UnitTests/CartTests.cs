using Shop.Domain.Core;
using Shop.Domain.Core.Exceptions;

namespace Shop.UnitTests
{
    public class CartTests
    {
        [Fact]
        public void AddItem_WhenItemIsNew_AddsItToCart()
        {
            // Arrange
            var cart = new Cart { Id = Guid.NewGuid() };
            var item = new CartItem { Id = 1, Name = "Book", Price = 9.99m };

            // Act
            cart.AddItem(item);

            // Assert
            Assert.Single(cart.Items);
            Assert.Contains(item, cart.Items);
        }

        [Fact]
        public void AddItem_WhenItemIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var cart = new Cart { Id = Guid.NewGuid() };

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => cart.AddItem(null!));
        }

        [Fact]
        public void AddItem_WhenItemWithSameIdAlreadyExists_ThrowsDuplicateEntityException()
        {
            // Arrange
            var cart = new Cart { Id = Guid.NewGuid() };
            var first = new CartItem { Id = 1, Name = "Book", Price = 9.99m };
            var duplicate = new CartItem { Id = 1, Name = "Another Book", Price = 14.99m };
            cart.AddItem(first);

            // Act + Assert
            Assert.Throws<DuplicateEntityException>(() => cart.AddItem(duplicate));
        }
    }
}

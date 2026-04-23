using LiteDB;
using Shop.DataAccess.LightDB;
using Shop.Domain.Core;

namespace Shop.IntegrationTests
{
    public sealed class CartRepositoryTests : IDisposable
    {
        private readonly ILiteDatabase _database;
        private readonly CartRepository _repository;

        public CartRepositoryTests()
        {
            _database = new LiteDatabase("Filename=:memory:");
            _repository = new CartRepository(_database);
        }

        public void Dispose() => _database.Dispose();

        [Fact]
        public void GetById_WhenCartDoesNotExist_ReturnsNull()
        {
            var result = _repository.GetById(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public void Save_ThenGetById_RoundTripsCartWithItems()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var cart = new Cart { Id = cartId };
            cart.AddItem(new CartItem
            {
                Id = 42,
                Name = "Widget",
                Image = new Image("http://example.com/w.png", "widget alt"),
                Price = 9.99m,
                Quantity = 2,
            });

            // Act
            _repository.Save(cart);
            var loaded = _repository.GetById(cartId);

            // Assert
            Assert.NotNull(loaded);
            Assert.Equal(cartId, loaded!.Id);

            var item = Assert.Single(loaded.Items);
            Assert.Equal(42, item.Id);
            Assert.Equal("Widget", item.Name);
            Assert.Equal(9.99m, item.Price);
            Assert.Equal(2, item.Quantity);
            Assert.NotNull(item.Image);
            Assert.Equal("http://example.com/w.png", item.Image!.Url);
            Assert.Equal("widget alt", item.Image.Text);
        }

        [Fact]
        public void Save_WhenCartAlreadyExists_UpsertsInsteadOfDuplicating()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var cart = new Cart { Id = cartId };
            cart.AddItem(new CartItem { Id = 1, Name = "A", Price = 1m, Quantity = 1 });
            _repository.Save(cart);

            // Act — reload, mutate, save again
            var reloaded = _repository.GetById(cartId)!;
            reloaded.AddItem(new CartItem { Id = 2, Name = "B", Price = 2m, Quantity = 3 });
            _repository.Save(reloaded);

            // Assert
            var finalCart = _repository.GetById(cartId);
            Assert.NotNull(finalCart);
            Assert.Equal(cartId, finalCart!.Id);
            Assert.Equal(2, finalCart.Items.Count);
            Assert.Contains(finalCart.Items, i => i.Id == 1 && i.Name == "A");
            Assert.Contains(finalCart.Items, i => i.Id == 2 && i.Name == "B");
        }
    }
}

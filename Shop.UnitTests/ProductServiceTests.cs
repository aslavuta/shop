using Shop.Application.Services;
using Shop.Domain.Core;
using Shop.UnitTests.Fakes;

namespace Shop.UnitTests
{
    public class ProductServiceTests
    {
        private readonly InMemoryProductRepository _repository;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repository = new InMemoryProductRepository();
            _service = new ProductService(_repository);
        }

        [Fact]
        public void Add_AssignsIdAndPersistsAllFields()
        {
            // Arrange
            var product = new Product
            {
                Name = "Widget",
                Description = "<p>thing</p>",
                CategoryId = 5,
                Price = 9.99m,
                Amount = 3,
            };

            // Act
            var id = _service.Add(product);
            var loaded = _service.Get(id);

            // Assert
            Assert.True(id > 0);
            Assert.NotNull(loaded);
            Assert.Equal("Widget", loaded!.Name);
            Assert.Equal(5, loaded.CategoryId);
            Assert.Equal(9.99m, loaded.Price);
            Assert.Equal(3, loaded.Amount);
        }

        [Fact]
        public void Add_WhenProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Add(null!));
        }

        [Fact]
        public void List_AfterMultipleAdds_ReturnsAllProducts()
        {
            // Arrange
            _service.Add(new Product { Name = "A", CategoryId = 1, Price = 1m, Amount = 1 });
            _service.Add(new Product { Name = "B", CategoryId = 1, Price = 2m, Amount = 1 });
            _service.Add(new Product { Name = "C", CategoryId = 2, Price = 3m, Amount = 1 });

            // Act
            var list = _service.List();

            // Assert
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void Delete_WhenProductDoesNotExist_ReturnsFalse()
        {
            var deleted = _service.Delete(404);

            Assert.False(deleted);
        }
    }
}

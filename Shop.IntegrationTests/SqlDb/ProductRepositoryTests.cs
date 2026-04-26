using Shop.DataAccess.SQLDB;
using Shop.Domain.Core;

namespace Shop.IntegrationTests.SqlDb
{
    public sealed class ProductRepositoryTests : IDisposable
    {
        private readonly SqliteDbContextFactory _fixture;
        private readonly ProductRepository _repository;
        private readonly int _categoryId;

        public ProductRepositoryTests()
        {
            _fixture = new SqliteDbContextFactory();
            _repository = new ProductRepository(_fixture.Context);

            var category = new Category { Name = "Books" };
            _fixture.Context.Categories.Add(category);
            _fixture.Context.SaveChanges();
            _categoryId = category.Id;
            _fixture.Context.ChangeTracker.Clear();
        }

        public void Dispose() => _fixture.Dispose();

        [Fact]
        public void Add_ThenGetById_RoundTripsProductWithAllFields()
        {
            // Arrange
            var product = new Product
            {
                Name = "Clean Architecture",
                Description = "<p>book</p>",
                Image = "http://img/ca.png",
                CategoryId = _categoryId,
                Price = 34.50m,
                Amount = 12,
            };

            // Act
            var id = _repository.Add(product);
            _fixture.Context.ChangeTracker.Clear();
            var loaded = _repository.GetById(id);

            // Assert
            Assert.NotNull(loaded);
            Assert.Equal("Clean Architecture", loaded!.Name);
            Assert.Equal("<p>book</p>", loaded.Description);
            Assert.Equal(_categoryId, loaded.CategoryId);
            Assert.Equal(34.50m, loaded.Price);
            Assert.Equal(12, loaded.Amount);
        }

        [Fact]
        public void Update_PersistsAllMutableFields()
        {
            // Arrange
            var id = _repository.Add(new Product
            {
                Name = "Old",
                CategoryId = _categoryId,
                Price = 1m,
                Amount = 1,
            });
            _fixture.Context.ChangeTracker.Clear();

            // Act
            var updated = _repository.Update(new Product
            {
                Id = id,
                Name = "New",
                Description = "fresh",
                CategoryId = _categoryId,
                Price = 99.99m,
                Amount = 7,
            });
            _fixture.Context.ChangeTracker.Clear();
            var loaded = _repository.GetById(id);

            // Assert
            Assert.True(updated);
            Assert.Equal("New", loaded!.Name);
            Assert.Equal("fresh", loaded.Description);
            Assert.Equal(99.99m, loaded.Price);
            Assert.Equal(7, loaded.Amount);
        }

        [Fact]
        public void Delete_RemovesProduct()
        {
            // Arrange
            var id = _repository.Add(new Product
            {
                Name = "Doomed",
                CategoryId = _categoryId,
                Price = 1m,
                Amount = 1,
            });
            _fixture.Context.ChangeTracker.Clear();

            // Act
            var deleted = _repository.Delete(id);
            var loaded = _repository.GetById(id);

            // Assert
            Assert.True(deleted);
            Assert.Null(loaded);
        }
    }
}

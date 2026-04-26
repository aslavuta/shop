using Shop.DataAccess.SQLDB;
using Shop.Domain.Core;

namespace Shop.IntegrationTests.SqlDb
{
    public sealed class CategoryRepositoryTests : IDisposable
    {
        private readonly SqliteDbContextFactory _fixture;
        private readonly CategoryRepository _repository;

        public CategoryRepositoryTests()
        {
            _fixture = new SqliteDbContextFactory();
            _repository = new CategoryRepository(_fixture.Context);
        }

        public void Dispose() => _fixture.Dispose();

        [Fact]
        public void Add_ThenGetById_RoundTripsCategory()
        {
            // Arrange
            var category = new Category { Name = "Electronics", Image = "http://img/e.png" };

            // Act
            var id = _repository.Add(category);
            _fixture.Context.ChangeTracker.Clear();
            var loaded = _repository.GetById(id);

            // Assert
            Assert.NotNull(loaded);
            Assert.Equal("Electronics", loaded!.Name);
            Assert.Equal("http://img/e.png", loaded.Image);
            Assert.Null(loaded.ParentId);
        }

        [Fact]
        public void Update_PersistsChanges()
        {
            // Arrange
            var id = _repository.Add(new Category { Name = "Original" });
            _fixture.Context.ChangeTracker.Clear();

            // Act
            var updated = _repository.Update(new Category { Id = id, Name = "Renamed" });
            _fixture.Context.ChangeTracker.Clear();
            var loaded = _repository.GetById(id);

            // Assert
            Assert.True(updated);
            Assert.Equal("Renamed", loaded!.Name);
        }

        [Fact]
        public void Delete_RemovesCategory()
        {
            // Arrange
            var id = _repository.Add(new Category { Name = "Doomed" });
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

using Shop.Application.Services;
using Shop.Domain.Core;
using Shop.UnitTests.Fakes;

namespace Shop.UnitTests
{
    public class CategoryServiceTests
    {
        private readonly InMemoryCategoryRepository _repository;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _repository = new InMemoryCategoryRepository();
            _service = new CategoryService(_repository);
        }

        [Fact]
        public void Add_AssignsIdAndMakesCategoryRetrievable()
        {
            // Arrange
            var category = new Category { Name = "Books" };

            // Act
            var id = _service.Add(category);
            var loaded = _service.Get(id);

            // Assert
            Assert.True(id > 0);
            Assert.NotNull(loaded);
            Assert.Equal("Books", loaded!.Name);
        }

        [Fact]
        public void Add_WhenCategoryIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Add(null!));
        }

        [Fact]
        public void Update_WhenCategoryDoesNotExist_ReturnsFalse()
        {
            var ghost = new Category { Id = 999, Name = "Ghost" };

            var updated = _service.Update(ghost);

            Assert.False(updated);
        }

        [Fact]
        public void Delete_WhenCategoryExists_RemovesIt()
        {
            // Arrange
            var id = _service.Add(new Category { Name = "Temp" });

            // Act
            var deleted = _service.Delete(id);

            // Assert
            Assert.True(deleted);
            Assert.Null(_service.Get(id));
        }
    }
}

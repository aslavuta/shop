using Microsoft.EntityFrameworkCore;
using Shop.Application.Abstractions;
using Shop.Domain.Core;

namespace Shop.DataAccess.SQLDB;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Category? GetById(int id) => _db.Categories.Find(id);

    public IReadOnlyList<Category> List() => _db.Categories.AsNoTracking().ToList();

    public int Add(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        _db.Categories.Add(category);
        _db.SaveChanges();
        return category.Id;
    }

    public bool Update(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);

        var existing = _db.Categories.Find(category.Id);
        if (existing is null)
            return false;

        existing.Name = category.Name;
        existing.Image = category.Image;
        existing.ParentId = category.ParentId;

        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var existing = _db.Categories.Find(id);
        if (existing is null)
            return false;

        _db.Categories.Remove(existing);
        _db.SaveChanges();
        return true;
    }
}

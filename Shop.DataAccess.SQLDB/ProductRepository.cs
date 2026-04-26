using Microsoft.EntityFrameworkCore;
using Shop.Application.Abstractions;
using Shop.Domain.Core;

namespace Shop.DataAccess.SQLDB;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Product? GetById(int id) => _db.Products.Find(id);

    public IReadOnlyList<Product> List() => _db.Products.AsNoTracking().ToList();

    public int Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        _db.Products.Add(product);
        _db.SaveChanges();
        return product.Id;
    }

    public bool Update(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var existing = _db.Products.Find(product.Id);
        if (existing is null)
            return false;

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Image = product.Image;
        existing.CategoryId = product.CategoryId;
        existing.Price = product.Price;
        existing.Amount = product.Amount;

        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var existing = _db.Products.Find(id);
        if (existing is null)
            return false;

        _db.Products.Remove(existing);
        _db.SaveChanges();
        return true;
    }
}

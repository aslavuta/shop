using Microsoft.EntityFrameworkCore;
using Shop.Domain.Core;

namespace Shop.DataAccess.SQLDB;

internal static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext db)
    {
        SeedCategories(db);
        SeedProducts(db);
    }

    private static void SeedCategories(ApplicationDbContext db)
    {
        if (db.Categories.Any())
            return;

        var electronics = new Category { Name = "Electronics" };
        var books = new Category { Name = "Books" };
        var clothing = new Category { Name = "Clothing" };

        db.Categories.AddRange(electronics, books, clothing);
        db.SaveChanges();

        var phones = new Category { Name = "Phones", ParentId = electronics.Id };
        var laptops = new Category { Name = "Laptops", ParentId = electronics.Id };

        db.Categories.AddRange(phones, laptops);
        db.SaveChanges();
    }

    private static void SeedProducts(ApplicationDbContext db)
    {
        if (db.Products.Any())
            return;

        var phonesId = db.Categories.Single(c => c.Name == "Phones").Id;
        var laptopsId = db.Categories.Single(c => c.Name == "Laptops").Id;
        var booksId = db.Categories.Single(c => c.Name == "Books").Id;
        var clothingId = db.Categories.Single(c => c.Name == "Clothing").Id;

        db.Products.AddRange(
            new Product
            {
                Name = "iPhone 15",
                Description = "<p>Flagship smartphone.</p>",
                CategoryId = phonesId,
                Price = 999.00m,
                Amount = 25,
            },
            new Product
            {
                Name = "Galaxy S24",
                Description = "<p>Android flagship.</p>",
                CategoryId = phonesId,
                Price = 899.00m,
                Amount = 30,
            },
            new Product
            {
                Name = "MacBook Pro 14",
                Description = "<p>Apple silicon laptop.</p>",
                CategoryId = laptopsId,
                Price = 1999.00m,
                Amount = 10,
            },
            new Product
            {
                Name = "Clean Architecture",
                Description = "<p>Book by Robert C. Martin.</p>",
                CategoryId = booksId,
                Price = 34.50m,
                Amount = 100,
            },
            new Product
            {
                Name = "Black T-Shirt",
                Description = "<p>100% cotton.</p>",
                CategoryId = clothingId,
                Price = 19.99m,
                Amount = 200,
            }
        );

        db.SaveChanges();
    }
}

using LiteDB;
using Shop.Application.Abstractions;
using Shop.Domain.Core;

namespace Shop.DataAccess.LightDB;

internal sealed class CartRepository : ICartRepository
{
    private const string CollectionName = "carts";

    private readonly ILiteCollection<Cart> _collection;

    public CartRepository(ILiteDatabase database)
    {
        _collection = database.GetCollection<Cart>(CollectionName);
    }

    public Cart? GetById(Guid id) => _collection.FindById(id);

    public void Save(Cart cart)
    {
        ArgumentNullException.ThrowIfNull(cart);
        _collection.Upsert(cart);
    }
}

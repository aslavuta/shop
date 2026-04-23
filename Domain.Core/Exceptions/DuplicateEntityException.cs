namespace Shop.Domain.Core.Exceptions;

public sealed class DuplicateEntityException : Exception
{
    public string EntityType { get; }

    public object Key { get; }

    public DuplicateEntityException(string entityType, object key)
        : this(entityType, key, innerException: null)
    {
    }

    public DuplicateEntityException(string entityType, object key, Exception? innerException)
        : base($"An entity of type '{entityType}' with key '{key}' already exists.", innerException)
    {
        EntityType = entityType;
        Key = key;
    }
}

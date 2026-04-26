namespace Shop.DataAccess.SQLDB;

public sealed class SqlDbOptions
{
    public const string SectionName = "SqlDB";

    public required string ConnectionString { get; init; }
}

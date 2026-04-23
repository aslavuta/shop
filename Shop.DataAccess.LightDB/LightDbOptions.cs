namespace Shop.DataAccess.LightDB;

public sealed class LightDbOptions
{
    public const string SectionName = "LightDB";

    public required string Path { get; init; }
}

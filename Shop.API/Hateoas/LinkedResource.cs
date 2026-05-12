using System.Text.Json.Serialization;

namespace Shop.API.Hateoas;

public sealed class LinkedResource<T>
{
    public required T Data { get; init; }

    [JsonPropertyName("_links")]
    public required IDictionary<string, Link> Links { get; init; }
}

using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shop.API.Swagger;

internal sealed class ApiVersionHeaderOperationFilter : IOperationFilter
{
    private const string HeaderName = "api-version";

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<IOpenApiParameter>();

        for (var i = operation.Parameters.Count - 1; i >= 0; i--)
        {
            var existing = operation.Parameters[i];
            if (existing.In == ParameterLocation.Header &&
                string.Equals(existing.Name, HeaderName, StringComparison.OrdinalIgnoreCase))
            {
                operation.Parameters.RemoveAt(i);
            }
        }

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = HeaderName,
            In = ParameterLocation.Header,
            Required = true,
            Description = "API version (e.g. 1.0). Sent as a request header.",
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Default = JsonValue.Create(context.ApiDescription.GroupName),
            },
        });
    }
}

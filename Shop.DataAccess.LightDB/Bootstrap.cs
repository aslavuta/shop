using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Abstractions;

namespace Shop.DataAccess.LightDB;

public static class Bootstrap
{
    private const string DatabaseFileName = "ShopNoSQL.db";

    public static IServiceCollection AddLightDb(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var section = configuration.GetSection(LightDbOptions.SectionName);
        var options =
            section.Get<LightDbOptions>()
            ?? throw new InvalidOperationException(
                $"Configuration section '{LightDbOptions.SectionName}' is missing."
            );

        if (string.IsNullOrWhiteSpace(options.Path))
        {
            throw new InvalidOperationException(
                $"'{LightDbOptions.SectionName}:{nameof(LightDbOptions.Path)}' must be provided."
            );
        }

        Directory.CreateDirectory(options.Path);
        var databasePath = System.IO.Path.Combine(options.Path, DatabaseFileName);

        services.AddSingleton<ILiteDatabase>(_ => new LiteDatabase(databasePath));
        services.AddScoped<ICartRepository, CartRepository>();

        return services;
    }
}

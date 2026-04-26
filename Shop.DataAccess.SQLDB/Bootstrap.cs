using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Abstractions;

namespace Shop.DataAccess.SQLDB;

public static class Bootstrap
{
    public static IServiceCollection AddSqlDb(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var section = configuration.GetSection(SqlDbOptions.SectionName);
        var options =
            section.Get<SqlDbOptions>()
            ?? throw new InvalidOperationException(
                $"Configuration section '{SqlDbOptions.SectionName}' is missing."
            );

        if (string.IsNullOrWhiteSpace(options.ConnectionString))
        {
            throw new InvalidOperationException(
                $"'{SqlDbOptions.SectionName}:{nameof(SqlDbOptions.ConnectionString)}' must be provided."
            );
        }

        services.AddDbContext<ApplicationDbContext>(db =>
            db.UseSqlServer(options.ConnectionString)
        );

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }

    public static IServiceProvider MigrateAndSeedSqlDb(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
        DatabaseSeeder.Seed(db);
        return services;
    }
}

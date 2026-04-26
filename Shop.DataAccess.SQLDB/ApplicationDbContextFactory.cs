using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shop.DataAccess.SQLDB;

internal sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var mdfPath = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), "..", "RelationalDB", "ShopSQL.mdf")
        );

        var connectionString =
            $"Server=(localdb)\\MSSQLLocalDB;AttachDbFilename={mdfPath};Database=ShopSQL;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new ApplicationDbContext(options);
    }
}

using Cinecerb.Client.Shared.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Cinecerb.Client.Platforms.Windows;

internal static class AppDatabaseStorage
{
    public static void Configure(IServiceCollection services)
    {
        services.AddDbContextFactory<AppDbContext>(options =>
        {
            Directory.CreateDirectory(AppDataPaths.Root);
            var connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = AppDataPaths.Database
            }.ToString();
            options.UseSqlite(connectionString);
        });
    }
}

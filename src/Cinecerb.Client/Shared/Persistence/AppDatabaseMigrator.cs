using Microsoft.EntityFrameworkCore;

namespace Cinecerb.Client.Shared.Persistence;

public sealed class AppDatabaseMigrator(IDbContextFactory<AppDbContext> contextFactory)
{
    public async Task MigrateAsync()
    {
        await Task.Run(async () =>
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            await context.Database.MigrateAsync();
        });
    }
}

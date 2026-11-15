using Cinecerb.Client.Features.Login.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cinecerb.Client.Shared.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
    }
}

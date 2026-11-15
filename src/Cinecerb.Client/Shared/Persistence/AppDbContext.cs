using Microsoft.EntityFrameworkCore;

namespace Cinecerb.Client.Shared.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options);

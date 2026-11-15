using Cinecerb.Client.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cinecerb.Client.Features.Login.Persistence;

public sealed class PlaylistStore(IDbContextFactory<AppDbContext> contextFactory)
{
    public Task<Playlist?> ReadLastUsedAsync()
    {
        return Task.Run(async () =>
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.Set<Playlist>().AsNoTracking()
                .FirstOrDefaultAsync(item => item.IsLastUsed);
        });
    }

    public Task SaveLoginAsync(Playlist login, bool save)
    {
        return Task.Run(async () =>
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var playlists = context.Set<Playlist>();
            var playlist = await playlists
                .FirstOrDefaultAsync(item => item.ServerAddress.ToLower() == login.ServerAddress.ToLower() 
                    && item.Username.ToLower() == login.Username.ToLower());

            if (playlist is null)
            {
                playlist = new Playlist
                {
                    Name = login.Name,
                    ServerAddress = login.ServerAddress,
                    Username = login.Username,
                    Password = login.Password,
                    IsTemporary = !save
                };
                playlists.Add(playlist);
            }
            else
            {
                playlist.Password = login.Password;

                if (save)
                {
                    playlist.Name = login.Name;
                    playlist.IsTemporary = false;
                }
            }

            if (!playlist.IsTemporary)
            {
                var previous = await playlists.ExecuteUpdateAsync(item => item.SetProperty(p => p.IsLastUsed, false));

                playlist.IsLastUsed = true;
            }

            await context.SaveChangesAsync();
        });
    }

    public Task DeleteTemporaryAsync()
    {
        return Task.Run(async () =>
        {
            using var context = contextFactory.CreateDbContext();
            context.Set<Playlist>().Where(item => item.IsTemporary).ExecuteDelete();
        });
    }
}

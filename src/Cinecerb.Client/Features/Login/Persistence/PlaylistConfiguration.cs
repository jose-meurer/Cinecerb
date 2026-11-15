using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinecerb.Client.Features.Login.Persistence;

public sealed class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> playlist)
    {
        playlist.ToTable("Playlists");
        playlist.HasKey(item => item.Id);
        playlist.Property(item => item.Id).ValueGeneratedOnAdd();

        playlist.Property(item => item.Name).IsRequired();
        playlist.Property(item => item.ServerAddress).IsRequired();
        playlist.Property(item => item.Username).IsRequired();
        playlist.Property(item => item.Password).IsRequired();
        playlist.Property(item => item.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}

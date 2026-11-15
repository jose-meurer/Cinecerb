namespace Cinecerb.Client.Features.Login.Persistence;

public sealed class Playlist
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ServerAddress { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsLastUsed { get; set; }
    public bool IsTemporary { get; set; }
}

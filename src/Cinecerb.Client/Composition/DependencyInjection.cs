using Cinecerb.Client.Features.Login;
using Cinecerb.Client.Features.Login.Persistence;
using Cinecerb.Client.Shared.Persistence;

namespace Cinecerb.Client.Composition;

public static class DependencyInjection
{
    public static IServiceCollection AddCinecerbServices(this IServiceCollection services)
    {
        services.AddSingleton(_ => new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        }));

        services.AddSingleton<AppDatabaseMigrator>();
        services.AddSingleton<PlaylistStore>();

        return services;
    }
}

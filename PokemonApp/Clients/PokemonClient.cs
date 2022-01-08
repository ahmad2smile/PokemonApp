using Polly;

namespace PokemonApp.Clients;

public static class PokemonClient
{
    public const string Name = nameof(PokemonClient);

    public static void AddPokemonClient(this IServiceCollection service)
    {
        service.AddHttpClient(
            Name, client =>
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon-species/");
                client.Timeout = TimeSpan.FromSeconds(30);
            }
        ).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));
    }
}
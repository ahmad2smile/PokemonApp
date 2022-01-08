using Polly;

namespace PokemonApp.Clients;

public static class FunTranslationClient
{
    public const string Name = nameof(FunTranslationClient);

    public static void AddFunTranslationClient(this IServiceCollection service)
    {
        service.AddHttpClient(
            Name, client =>
            {
                client.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36 Edg/97.0.1072.55"
                );
            }
        ).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));
    }
}
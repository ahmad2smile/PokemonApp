using PokemonApp.Clients;
using PokemonApp.Models;

namespace PokemonApp.Services;

public class FunTranslatorService : BaseTranslator, IFunTranslatorService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<FunTranslatorService> _logger;

    public FunTranslatorService(IHttpClientFactory httpClientFactory, ILogger<FunTranslatorService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public override async Task<string> Translate(string message, TranslatorLang lang)
    {
        try
        {
            var client = _httpClientFactory.CreateClient(FunTranslationClient.Name);
            var result =
                await client.PostAsJsonAsync(
                    $"{lang.ToString().ToLower()}.json",
                    new { text = message }
                );

            var response = await result.Content.ReadFromJsonAsync<FunTranslationsApiResponse>();

            return response?.contents?.translated ?? message;
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Something went wrong while Translating {Message}, {ExceptionMessage}", message, e.Message
            );
            return message;
        }
    }
}
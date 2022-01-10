using PokemonApp.Clients;
using PokemonApp.Models;

namespace PokemonApp.Services;

public class FunTranslatorService : BaseTranslator, IFunTranslatorService
{
    private readonly ICacheService _cacheService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<FunTranslatorService> _logger;

    public FunTranslatorService(
        IHttpClientFactory httpClientFactory,
        ILogger<FunTranslatorService> logger,
        ICacheService cacheService
    )
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _cacheService = cacheService;
    }

    public override async Task<string> Translate(string message, TranslatorLang lang)
    {
        try
        {
            var cachedValue = (string?)await _cacheService.Get(message);

            if (cachedValue is not null)
            {
                _logger.LogDebug("Cache hit for {Message}", message);

                return cachedValue;
            }

            var client = _httpClientFactory.CreateClient(FunTranslationClient.Name);
            var result =
                await client.PostAsJsonAsync(
                    $"{lang.ToString().ToLower()}.json",
                    new { text = message }
                );

            var response = await result.Content.ReadFromJsonAsync<FunTranslationsApiResponse>();

            var translatedMessage = response?.contents?.translated ?? message;

            await _cacheService.Set(message, translatedMessage, TimeSpan.FromDays(1));

            return translatedMessage;
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
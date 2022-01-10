using System.Net;
using PokemonApp.Clients;
using PokemonApp.Models;
using PokemonApp.Utils;

namespace PokemonApp.Services;

public class PokemonService : IPokemonService
{
    private readonly ICacheService _cacheService;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IFunTranslatorService _funTranslatorService;
    private readonly ILogger<PokemonService> _logger;

    public PokemonService(
        IHttpClientFactory clientFactory,
        IFunTranslatorService funTranslatorService,
        ICacheService cacheService,
        ILogger<PokemonService> logger
    )
    {
        _clientFactory = clientFactory;
        _funTranslatorService = funTranslatorService;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<PokemonResponse> GetByName(string name)
    {
        try
        {
            var cachedValue = await _cacheService.GetObject<Pokemon>(name);

            if (cachedValue is not null)
            {
                _logger.LogDebug("Cache hit for {PokemonName}", name);

                return cachedValue;
            }

            var client = _clientFactory.CreateClient(PokemonClient.Name);

            var result = await client.GetFromJsonAsync<PokemonApiRawResponse>(name);

            if (result is null)
            {
                return new UnknownPokemonError(name);
            }

            var description = result.flavor_text_entries?.First().flavor_text;

            var pokemon = new Pokemon(
                result.name, description, result.habitat?.name, result.is_legendary
            );

            await _cacheService.SetObject(name, pokemon, TimeSpan.FromDays(1));

            return pokemon;
        }
        catch (Exception e)
        {
            if (e is HttpRequestException { StatusCode: HttpStatusCode.NotFound })
            {
                return new InvalidPokemonError();
            }

            return new UnknownPokemonError(name);
        }
    }

    public async Task<PokemonResponse> GetByNameTranslated(string name)
    {
        var result = await GetByName(name);

        if (!result.IsT0)
        {
            return result;
        }

        var pokemon = result.AsT0;

        var translator = TranslatorHelper.GetTranslatorVariant(pokemon);

        var description = pokemon.Description is not null
            ? await _funTranslatorService.Translate(pokemon.Description, translator)
            : string.Empty;

        return pokemon with { Description = description };
    }
}
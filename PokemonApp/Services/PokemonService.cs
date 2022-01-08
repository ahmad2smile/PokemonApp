using System.Net;
using PokemonApp.Clients;
using PokemonApp.Models;

namespace PokemonApp.Services;

public class PokemonService : IPokemonService
{
    private readonly IHttpClientFactory _clientFactory;

    public PokemonService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<PokemonResponse> GetByName(string name)
    {
        try
        {
            var client = _clientFactory.CreateClient(PokemonClient.Name);

            var result = await client.GetFromJsonAsync<PokemonApiRawResponse>(name);

            if (result is null)
            {
                return new UnknownPokemonError(name);
            }

            return new Pokemon(
                result.name, result.flavor_text_entries?.First().flavor_text, result.habitat?.name, result.is_legendary
            );
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
}
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NSubstitute;
using PokemonApp.Models;
using PokemonApp.Services;
using PokemonApp.Test.Unit.Helpers;
using Xunit;

namespace PokemonApp.Test.Unit.Services;

public class PokemonServiceTests
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly PokemonService _pokemonService;

    public PokemonServiceTests()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();

        _pokemonService = new PokemonService(_httpClientFactory);
    }

    [Fact(DisplayName = "Get Pokemon for Given Name")]
    public async Task Should_return_Pokemon()
    {
        var mockPokemonName = "mock-name";
        var mockResponse = new PokemonApiRawResponse { name = mockPokemonName };

        var client = MockClientFactory.GetClient(new StringContent(JsonSerializer.Serialize(mockResponse)));
        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(client);

        var result = await _pokemonService.GetByName(mockPokemonName);

        Assert.Equal(mockPokemonName, result?.AsT0.Name);
    }

    [Fact(DisplayName = "Get Pokemon returns InvalidPokemonError when 404")]
    public async Task Should_return_InvalidPokemonError()
    {
        var client = MockClientFactory.GetClient(new HttpRequestException(string.Empty, null, HttpStatusCode.NotFound));
        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(client);

        var result = await _pokemonService.GetByName(string.Empty);

        Assert.IsType<InvalidPokemonError>(result?.AsT1);
    }

    [Fact(DisplayName = "Get Pokemon returns UnknownPokemonError when unknown Error")]
    public async Task Should_return_UnknownPokemonError()
    {
        var client = MockClientFactory.GetClient(new Exception());
        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(client);

        var result = await _pokemonService.GetByName(string.Empty);

        Assert.IsType<UnknownPokemonError>(result?.AsT2);
    }
}
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PokemonApp.Models;
using PokemonApp.Services;
using PokemonApp.Test.Unit.Helpers;
using Xunit;

namespace PokemonApp.Test.Unit.Services;

public class FunTranslatorServiceTests
{
    private readonly FunTranslatorService _funcTranslatorService;
    private readonly IHttpClientFactory _httpClientFactory;

    public FunTranslatorServiceTests()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        var logger = new Logger<FunTranslatorService>(new LoggerFactory());
        _funcTranslatorService = new FunTranslatorService(_httpClientFactory, logger);
    }

    [Fact(DisplayName = "Translate returns given message")]
    public async Task Translate_Returns_Message()
    {
        var mockResponse = new FunTranslationsApiResponse();
        var client =
            MockClientFactory.GetClient(new StringContent(JsonSerializer.Serialize(mockResponse)));
        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(client);
        var mockMessage = "mock-message";

        var result = await _funcTranslatorService.Translate(mockMessage, TranslatorLang.Shakespeare);

        Assert.Equal(mockMessage, result);
    }


    [Fact(DisplayName = "Translate returns given message even when API request failed")]
    public async Task Translate_Returns_Message_OnError()
    {
        var client = MockClientFactory.GetClient(new Exception("things went side way"));
        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(client);
        var mockMessage = "mock-message";

        var result = await _funcTranslatorService.Translate(mockMessage, TranslatorLang.Shakespeare);

        Assert.Equal(mockMessage, result);
    }
}
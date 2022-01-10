using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PokemonApp.Services;
using StackExchange.Redis;
using Xunit;

namespace PokemonApp.Test.Unit.Services;

public class CacheServiceTests
{
    private readonly CacheService _cacheService;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _redistDatabase;

    public CacheServiceTests()
    {
        _redistDatabase = Substitute.For<IDatabase>();
        _connectionMultiplexer = Substitute.For<IConnectionMultiplexer>();
        var logger = new Logger<CacheService>(new LoggerFactory());

        _cacheService = new CacheService(_connectionMultiplexer, logger);
    }

    [Fact(DisplayName = "Get for Key returns Value")]
    public async Task Get_Returns_String()
    {
        _redistDatabase.StringGetAsync(string.Empty).Returns(Task.FromResult(new RedisValue(string.Empty)));
        _connectionMultiplexer.GetDatabase().Returns(_redistDatabase);

        var result = (string?)await _cacheService.Get(string.Empty);

        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Get suppresses Error and returns null")]
    public async Task Get_Suppresses_Throw()
    {
        _redistDatabase.StringGetAsync(string.Empty).ThrowsForAnyArgs(new Exception("Mock Exception"));
        _connectionMultiplexer.GetDatabase().Returns(_redistDatabase);

        var result = (string?)await _cacheService.Get(string.Empty);

        Assert.Null(result);
    }


    [Fact(DisplayName = "Set suppresses Error and returns null")]
    public async Task Set_Suppresses_Throw()
    {
        _redistDatabase.StringSetAsync(string.Empty, string.Empty, TimeSpan.MaxValue)
            .ThrowsForAnyArgs(new Exception("Mock Exception"));
        _connectionMultiplexer.GetDatabase().Returns(_redistDatabase);

        var result =
            await Record.ExceptionAsync(() => _cacheService.Set(string.Empty, string.Empty, TimeSpan.MaxValue));

        Assert.Null(result);
    }
}
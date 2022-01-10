using System.Text.Json;
using StackExchange.Redis;

namespace PokemonApp.Services;

public class CacheService : ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IConnectionMultiplexer connectionMultiplexer, ILogger<CacheService> logger)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _logger = logger;
    }

    public async Task<RedisValue?> Get(string key)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();

            return await db.StringGetAsync(key);
        }
        catch (Exception e)
        {
            _logger.LogError("Something went wrong while try to fetch cache for {Key}, {Error}", key, e.Message);

            return null;
        }
    }

    public async Task<T?> GetObject<T>(string key)
    {
        var redisValue = (byte[]?)await Get(key);

        return redisValue is not null ? JsonSerializer.Deserialize<T>(redisValue) : default;
    }

    public async Task Set(string key, RedisValue value, TimeSpan? expiry)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();

            await db.StringSetAsync(key, value, expiry);
        }
        catch (Exception e)
        {
            _logger.LogError("Something went wrong while try to set cache for {Key}, {Error}", key, e.Message);
        }
    }

    public async Task SetObject<T>(string key, T value, TimeSpan? expiry)
    {
        await Set(key, JsonSerializer.SerializeToUtf8Bytes(value), expiry);
    }
}
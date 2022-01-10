using StackExchange.Redis;

namespace PokemonApp.Services;

public interface ICacheService
{
    Task<RedisValue?> Get(string key);
    Task Set(string key, RedisValue value, TimeSpan? expiry);
    Task<T?> GetObject<T>(string key);
    Task SetObject<T>(string key, T value, TimeSpan? expiry);
}
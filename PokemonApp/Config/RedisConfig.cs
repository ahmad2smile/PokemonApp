using StackExchange.Redis;

namespace PokemonApp.Config;

public static class RedisConfig
{
    public static void AddRedisConfig(this IServiceCollection service, ConfigurationManager configurations)
    {
        service.AddSingleton<IConnectionMultiplexer>(
            x =>
                ConnectionMultiplexer.Connect(
                    configurations.GetValue<string>("RedisConnection"),
                    // NOTE: In Docker if you try to resolve Service name, it can't as DNS resolution is disabled
                    // ref: https://github.com/StackExchange/StackExchange.Redis/issues/1002
                    options => options.ResolveDns = bool.TryParse(
                        Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var _
                    )
                )
        );
    }
}
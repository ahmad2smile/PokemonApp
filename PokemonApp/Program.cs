using PokemonApp.Clients;
using PokemonApp.Config;
using PokemonApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configurations = builder.Configuration;

var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddPokemonClient();
services.AddFunTranslationClient();
services.AddRedisConfig(configurations);

services.AddSingleton<IPokemonService, PokemonService>();
services.AddSingleton<IFunTranslatorService, FunTranslatorService>();
services.AddSingleton<ICacheService, CacheService>();

var app = builder.Build();

// NOTE: Adding for testing Production Docker build
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
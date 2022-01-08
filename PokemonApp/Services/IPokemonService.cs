using PokemonApp.Models;

namespace PokemonApp.Services;

public interface IPokemonService
{
    Task<PokemonResponse> GetByName(string name);
}
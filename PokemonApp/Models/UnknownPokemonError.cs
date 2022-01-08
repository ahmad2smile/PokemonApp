namespace PokemonApp.Models;

public class UnknownPokemonError
{
    public UnknownPokemonError(string name)
    {
        Message = $"Unable to fetch {name} Pokemon";
    }

    public string Message { get; }
}
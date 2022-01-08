using OneOf;

namespace PokemonApp.Models;

[GenerateOneOf]
public partial class PokemonResponse : OneOfBase<Pokemon, InvalidPokemonError, UnknownPokemonError>
{
}
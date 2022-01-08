using PokemonApp.Models;

namespace PokemonApp.Utils;

public class TranslatorHelper
{
    public static TranslatorLang GetTranslatorVariant(Pokemon pokemon)
    {
        return pokemon.IsLegendary || pokemon.Habitat == "cave"
            ? TranslatorLang.Yoda
            : TranslatorLang.Shakespeare;
    }
}
using PokemonApp.Models;
using PokemonApp.Utils;
using Xunit;

namespace PokemonApp.Test.Unit.Utils;

public class TranslatorHelperTests
{
    [Fact(DisplayName = "GetTranslatorVariant returns Yoda variant when Habitat Cave")]
    public void GetTranslatorVariant_Returns_Yoda_When_Cave()
    {
        var pokemon = new Pokemon("mock-name", "mock-description", "cave", false);

        Assert.Equal(TranslatorLang.Yoda, TranslatorHelper.GetTranslatorVariant(pokemon));
    }

    [Fact(DisplayName = "GetTranslatorVariant returns Yoda variant when Legendary")]
    public void GetTranslatorVariant_Returns_Yoda_When_Legendary()
    {
        var pokemon = new Pokemon("mock-name", "mock-description", "river", true);

        Assert.Equal(TranslatorLang.Yoda, TranslatorHelper.GetTranslatorVariant(pokemon));
    }


    [Fact(DisplayName = "GetTranslatorVariant returns Shakespeare variant when Habitat not cave and not Legendary")]
    public void GetTranslatorVariant_Returns_Shakespeare()
    {
        var pokemon = new Pokemon("mock-name", "mock-description", "river", false);

        Assert.Equal(TranslatorLang.Shakespeare, TranslatorHelper.GetTranslatorVariant(pokemon));
    }
}
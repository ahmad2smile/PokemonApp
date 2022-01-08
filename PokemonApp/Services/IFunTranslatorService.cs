using PokemonApp.Models;

namespace PokemonApp.Services;

public interface IFunTranslatorService
{
    Task<string> Translate(string message, TranslatorLang lang);
}
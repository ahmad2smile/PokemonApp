using PokemonApp.Models;

namespace PokemonApp.Services;

public abstract class BaseTranslator
{
    public abstract Task<string> Translate(string message, TranslatorLang lang);
}
namespace PokemonApp.Models;

// NOTE: Generated Content from API JSON Response
public class FunTranslationsApiResponse
{
    public FunTransSuccess success { get; set; }
    public FunTransContents? contents { get; set; }
}

public class FunTransSuccess
{
    public int total { get; set; }
}

public class FunTransContents
{
    public string translated { get; set; }
    public string text { get; set; }
    public string translation { get; set; }
}
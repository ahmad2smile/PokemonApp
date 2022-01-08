using Microsoft.AspNetCore.Mvc;

namespace PokemonApp.Utils;

public static class HttpResponseUtils
{
    public static IActionResult ServerError<T>(T? result)
    {
        return ServerError(result, 500);
    }

    public static IActionResult ServerError<T>(T? result, int code)
    {
        return new ObjectResult(result) { StatusCode = code };
    }
}
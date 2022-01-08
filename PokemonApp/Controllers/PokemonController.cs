using Microsoft.AspNetCore.Mvc;
using PokemonApp.Services;
using PokemonApp.Utils;

namespace PokemonApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string name)
    {
        var result = await _pokemonService.GetByName(name);

        return result.Match<IActionResult>(Ok, NotFound, HttpResponseUtils.ServerError);
    }
}
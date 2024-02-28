using HotOrCold.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotOrCold.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class DrinksController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<List<Drink>>> AddDrink([FromForm] Drink drink)
    {
        return Ok();
    }
    [HttpGet]
    public async Task<ActionResult<List<Drink>>> GetAllDrinks()
    {
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<List<Drink>>> GetDrinkById([FromRoute] int id)
    {
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<List<Drink>>> UpdateDrink()
    {
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Drink>>> DeleteDrink()
    {
        return Ok();
    }
}
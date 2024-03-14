using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotOrCold.Controllers;
[ApiController]
[Route("/api/[Controller]")]
public class CartsController(ICartRepository cartRepository) : ControllerBase
{
    private readonly ICartRepository _cartRepository = cartRepository;
    [HttpPost]
    public async Task<ActionResult<Cart>> AddDrinkCopyToCart([FromBody] AddDrinkCopyDto addDrinkCopyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(addDrinkCopyDto);
        }
        await _cartRepository.AddDrinkCopyToCart(addDrinkCopyDto);
        return Ok("Ajouté avec succèes");
    }
}
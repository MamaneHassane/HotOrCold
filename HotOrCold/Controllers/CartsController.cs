using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using HotOrCold.Security.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotOrCold.Controllers;
[ApiController]
[Route("/api/[Controller]")]
[RequiresClaim(IdentityData.CustomerPolicyName,IdentityData.CustomerClaimValue)]
public class CartsController(ICartRepository cartRepository) : ControllerBase
{
    private readonly ICartRepository _cartRepository = cartRepository;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart?>>> GetAllCarts()
    {
        try
        {
            return Ok(await _cartRepository.GetAll());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite dans la recherche de panier");
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cart?>> GetCartById(int id)
    {
        try
        {
            return Ok(await _cartRepository.Get(id));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite dans la recherche de panier");
        }
    }
    
    [HttpPost("addDrinkCopy")]
    public async Task<ActionResult<Cart?>> AddDrinkCopyToCart([FromBody] AddDrinkCopyDto addDrinkCopyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(addDrinkCopyDto);
        }
        await _cartRepository.AddDrinkCopyToCart(addDrinkCopyDto);
        return Ok("Ajouté avec succèes");
    }
    
    [HttpPost("addManyDrinkCopies")]
    public async Task<ActionResult<Cart?>> AddManyDrinkCopiesToCart([FromBody] AddManyDrinkCopiesDto addManyDrinkCopiesDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(addManyDrinkCopiesDto);
        }
        try
        {
            
            await _cartRepository.AddManyDrinkCopies(addManyDrinkCopiesDto);
            return Ok("Ajoutées avec succès");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite dans la recherche de panier");
        }
    }

    [HttpPost("removeDrinkCopyFromCart")]
    public async Task<ActionResult<Cart?>> RemoveDrinkCopyCart([FromBody] RemoveDrinkCopyFromCartDto removeDrinkCopyFromCartDto)
    {
        try
        {
            return Ok(await _cartRepository.RemoveDrinkCopy(removeDrinkCopyFromCartDto));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la suppression de la boisson du panier");
        }
    }
}
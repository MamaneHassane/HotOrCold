using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using HotOrCold.Security.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotOrCold.Controllers;

[ApiController]
[Route("/api/[controller]")]
[RequiresClaim(IdentityData.AdminPolicyName,IdentityData.AdminClaimValue)]
public class DrinksController(IDrinkRepository drinkRepository) : ControllerBase
{
    private readonly IDrinkRepository _drinkRepository = drinkRepository;
    [HttpPost]
    public async Task<ActionResult<Drink>> AddDrink([FromForm] Drink drink)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }
        try
        {
            var createdDrink = await _drinkRepository.Create(drink);
            return Ok(createdDrink); 
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Erreur d'ajout de la boisson: {exception.Message}"); 
            return  StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la création de la boisson"); 
        }
    }
    [HttpGet]
    public async Task<ActionResult<List<Drink>>> GetAllDrinks()
    {
        try
        {
            return Ok(await _drinkRepository.GetAll());
        }
        catch(Exception exception)
        {
            Console.WriteLine($"Erreur de recherche des boissons: {exception.Message}"); 
            return  StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la recherche des boissons"); 
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Drink?>> GetDrinkById([FromRoute] int id)
    {
        try
        {
            return Ok(await _drinkRepository.Get(id));
        }
        catch(Exception exception)
        {
            Console.WriteLine($"Erreur de recherche de la boisson: {exception.Message}"); 
            return  StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la recherche de la boisson"); 
        }   
    }
    [HttpPut]
    public async Task<ActionResult<List<Drink>>> UpdateDrink(int id, [FromForm] Drink updatedDrink)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try 
        {
            return Ok(await _drinkRepository.Update(updatedDrink));
        }
        catch(Exception exception)
        {
            Console.WriteLine($"Erreur de mise à jour de la boisson: {exception.Message}"); 
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la mise à jour de la boisson"); 
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteDrink(int id)
    {
        try
        {
            if(await _drinkRepository.Delete(id) is true)
            {
                return Ok("La boisson est effacée");
            } 
        }
        catch(Exception exception)
        {
            Console.WriteLine($"Une erreur dans la suppression {exception.Message} ");
            return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
        }
        return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la suppression");
    }
}
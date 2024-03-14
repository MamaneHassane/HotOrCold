using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotOrCold.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DrinkCopiesController(IDrinkCopyRepository drinkCopyRepository) : ControllerBase
{
    private readonly IDrinkCopyRepository _drinkCopyRepository = drinkCopyRepository;

    [HttpPost]
    public async Task<ActionResult<DrinkCopy>> Create([FromBody] DrinkCopy drinkCopy)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(drinkCopy);
        }
        try
        {
            
            // return Created($"/api/drinkcopies/{drinkCopy.DrinkId}",await _drinkCopyRepository.Create(drinkCopy));
            return Ok(await _drinkCopyRepository.Create(drinkCopy));
        }
        catch (Exception exception)
        {
            Console.WriteLine("id dans le controlleur");
            Console.WriteLine("quantité dans le controlleur "+ drinkCopy.QuantityInLiter);
            Console.WriteLine($"Une erreur s'est produite lors de la création de la boisson: {exception.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Une erreur s'est produite lors de la création de l'exemplaire");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<DrinkCopy>>> GetAll()
    {
        try
        {
            return Ok(await _drinkCopyRepository.GetAll());
        }
        catch(Exception exception)
        {
            Console.WriteLine($"Une erreur s'est produite lors de la recherche des exemplaires de boissons: {exception.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Une erreur s'est produite lors de la recherche des exemplaires de boissons");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DrinkCopy>> Get(int id)
    {
        try
        {
            return Ok(await _drinkCopyRepository.Get(id));
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Une erreur s'est produite lors de la recherche d'un exemplaire de boisson : {exception.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Une erreur s'est produite lors de la recherche d'un exemplaire de boisson");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DrinkCopy>> UpdateCopy(DrinkCopy updatedDrinkCopy)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try 
        {
            return Ok(await _drinkCopyRepository.Update(updatedDrinkCopy));
        }
        catch(Exception exception)
        {
            Console.WriteLine($"Erreur de mise à jour de l'exemplaire de boisson: {exception.Message}"); 
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la mise à jour de la boisson"); 
        }
    }
    
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteDrink(int id)
    {
        try
        {
            if(await _drinkCopyRepository.Delete(id) is true)
            {
                return Ok("L'exemplaire de boisson est effacé");
            } 
        }
        catch(Exception exception)
        {
            Console.WriteLine($"Une erreur dans la suppression {exception.Message} ");
            return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
        }
        return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la suppression de l'exemplaire de boisson");
    }
}
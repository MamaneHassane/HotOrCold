using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CommandsController(ICommandRepository commandRepository) : ControllerBase
{
    private readonly ICommandRepository _commandRepository = commandRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Command>?>> GetAllCommands()
    {
        try
        {
            return Ok(await _commandRepository.GetAllCommands());
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite dans la récupération des commandes");
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Command>> GetCommandById(int id)
    {
        try
        {
            return Ok(await _commandRepository.Get(id));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Un erreur s'est produite dans la recherche de la commande");
        }        
    }
    
    [HttpPost("doCommand")]
    public async Task<ActionResult<Command>> DoCommandAndClearCart([FromBody] DoCommandAndClearCartDto doCommandAndClearCartDto)
    {
        try
        {
            var result = await _commandRepository.DoCommandAndClearCart(doCommandAndClearCartDto);
            return result is false ? StatusCode(StatusCodes.Status406NotAcceptable,"Vous n'avez pas assez d'argent ou réessayer plus tard") : Ok("La commande à été passée avec succeès");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Un erreur s'est produite lors de la passation de la commande");
        }        
    }

    [HttpPost("confirmCommand")]
    public async Task<ActionResult<Command>> ConfirmCommand([FromBody] ConfirmCommandDeliveredAndPayDto confirmCommandDeliveredAndPayDto)
    {
        try
        {
            return Ok(await _commandRepository.ConfirmCommandDeliveredAndPay(confirmCommandDeliveredAndPayDto));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la confirmation de la commande.");
        }
    }

    [HttpPost("cancelCommand")]
    public async Task<ActionResult<Command>> CancelCommand([FromBody] CancelCommandDto cancelCommandDto)
    {
        try
        {
            return Ok(await _commandRepository.CancelCommand(cancelCommandDto));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur s'est rpoduite lors de l'annulation de la commande");
        }
    }
}
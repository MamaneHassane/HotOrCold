using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using HotOrCold.Security.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Controllers;

[ApiController]
[Route("/api/[controller]")]
[RequiresClaim(IdentityData.CustomerPolicyName,IdentityData.CustomerClaimValue)]
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
    public async Task<ActionResult<Command>> ConfirmCommand([FromBody] ConfirmCommandDeliveredAndPayedDto confirmCommandDeliveredAndPayedDto)
    {
        try
        {
            return Ok(await _commandRepository.ConfirmCommandDeliveredAndPayed(confirmCommandDeliveredAndPayedDto));
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
                "Une erreur s'est produite lors de l'annulation de la commande");
        }
    }

    [HttpGet("getActivesCommands/{id:int}")]
    public async Task<ActionResult<IEnumerable<Command?>?>> GetActivesCommandByCustomerId(int id)
    {
        try
        {
            return Ok(await _commandRepository.GetActivesCommandByCustomerId(id));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur s'est produite lors de la recherche des commandes en cours");
        }
    }
    
    [HttpGet("getHistorizedCommands/{id:int}")]
    public async Task<ActionResult<IEnumerable<Command?>?>> GetHistorizedCommandByCustomerId(int id)
    {
        try
        {
            return Ok(await _commandRepository.GetHistorizedCommandByCustomerId(id));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur s'est produite lors de la recherche des anciennes commandes");
        }
    }
}
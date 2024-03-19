using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using HotOrCold.Security.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotOrCold.Controllers;
[ApiController]
[Route("/api/administrators")]
[RequiresClaim(IdentityData.AdminPolicyName,IdentityData.AdminClaimValue)]
public class AdministratorController(IAdministratorRepository administratorRepository) : ControllerBase
{
    private readonly IAdministratorRepository _administratorRepository = administratorRepository;
    
    [HttpPost("register")]
    public async Task<ActionResult<Customer>> Register([FromBody] Administrator administrator)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(administrator);
        }
        try
        {
            await _administratorRepository.Register(administrator);
            return Created("/api/administrators/"+administrator.AdministratorId,administrator);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            Console.WriteLine("Erreur lors de la création de compte administrateur");
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la création du client");
        }
    }
}
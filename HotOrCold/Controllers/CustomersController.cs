using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using HotOrCold.Security.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotOrCold.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CustomersController(ICustomerRepository customerRepository) : ControllerBase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<Customer>> Register([FromBody] Customer customer)
    {
        Console.WriteLine("Reached here");
        Console.WriteLine(customer.Username);
        if (!ModelState.IsValid)
        {
            return BadRequest(customer);
        }
        try
        {
            await _customerRepository.Register(customer);
            return Created("/api/customers/"+customer.CustomerId,customer);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            Console.WriteLine("Erreur lors de la création de compte utilisateur");
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la création du client");
        }
    }

    [HttpGet("{id:int}")]
    [RequiresClaim(IdentityData.CustomerPolicyName,IdentityData.CustomerClaimValue)]
    public async Task<ActionResult<Customer>> ConsultAccount(int id)
    {
        var customer = await _customerRepository.Get(id);
        if (customer is null) return NotFound("Le client n'existe pas dans la base de données");
        return Ok(customer);
    }

    [HttpGet]
    [RequiresClaim(IdentityData.AdminPolicyName,IdentityData.AdminClaimValue)]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        try
        {
            return Ok(await _customerRepository.GetAll());
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite en listant les clients");
        }
    }

    [RequiresClaim(IdentityData.CustomerPolicyName,IdentityData.CustomerClaimValue)]
    [HttpPost("increaseBalance/{id:int}/{amount:double}")]
    public async Task<ActionResult<bool>> IncreaseCustomerBalance(int id, double amount)
    {
        try
        {
            return Ok(await _customerRepository.IncreaseBalance(id, amount));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur s'est produite lors de l'ajout de l'argent");
        }
    }

    [HttpDelete("delete/{id:int}")]
    [RequiresClaim(IdentityData.CustomerPolicyName,IdentityData.CustomerClaimValue)]
    public async Task<ActionResult<bool>> DeleteAccount(int id)
    {
       var deleted = await _customerRepository.Delete(id);
       return deleted ? Ok("Le client à été supprimé avec succès") : StatusCode(StatusCodes.Status500InternalServerError, "Il y'a eu une erreur, veuillez réessayer plus tard");
    }
    
}
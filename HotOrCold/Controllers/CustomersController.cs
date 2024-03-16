using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotOrCold.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CustomersController(ICustomerRepository customerRepository) : ControllerBase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    [HttpPost("register")]
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

    [HttpPost("authenticate")]
    public async Task<ActionResult<Customer?>> Authenticate([FromBody] CustomerAuthenticationDto customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Il y'a une erreur dans la requête, réessayez plus tard");
        }
        var customerFound = await _customerRepository.Authenticate(customer);
        if (customerFound is null) return NotFound("Le nom d'utilisateur ou mot de passe est incorrecte");
        return Ok(customerFound);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Customer>> ConsultAccount(int id)
    {
        var customer = await _customerRepository.Get(id);
        if (customer is null) return NotFound("Le client n'existe pas dans la base de données");
        return Ok(customer);
    }

    [HttpGet]
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
    public async Task<ActionResult<bool>> DeleteAccount(int id)
    {
       var deleted = await _customerRepository.Delete(id);
       return deleted ? Ok("Le client à été supprimé avec succès") : StatusCode(StatusCodes.Status500InternalServerError, "Il y'a eu une erreur, veuillez réessayer plus tard");
    }
    
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotOrCold.Repositories.Interfaces;
using HotOrCold.Security.Identity;
using HotOrCold.Security.Models;
using HotOrCold.Security.StaticDatas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HotOrCold.Security.Controllers;

[ApiController]
[Route("/api/authentications")]
public class IdentityController(ICustomerRepository customerRepository, IAdministratorRepository administratorRepository) : ControllerBase
{
    // Les repositories
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IAdministratorRepository _administratorRepository = administratorRepository;
    
    // Méthode d'authentification des clients, accessible par tout le monde
    [AllowAnonymous]
    [HttpPost("authenticateCustomerAndGetAccessToken")]
    public async Task<IActionResult> GenerateTokenForCustomer([FromBody] TokenGenerationRequest tokenGenerationRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Il y'a une erreur dans la requête, réessayez plus tard");
        }
        var customerFound = await _customerRepository.Authenticate(tokenGenerationRequest);
        if (customerFound is null) return NotFound("Le nom d'utilisateur ou mot de passe est incorrecte");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(StaticSecurityDatas.SecretKey);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Sub, customerFound.Email),
            new (JwtRegisteredClaimNames.Email, customerFound.Email),
            new ("userid", customerFound.CustomerId.ToString())
        };
        
        // On utilise un seul Claim pour toutes les méthodes du customer
        var claim = new Claim(IdentityData.CustomerPolicyName, IdentityData.CustomerClaimValue, ClaimValueTypes.String);
        claims.Add(claim);
        
        // Le descripteur de jeton
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(StaticSecurityDatas.TokenLifeTime),
            Issuer = StaticSecurityDatas.Issuer,
            Audience = StaticSecurityDatas.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),StaticSecurityDatas.SecurityAlgorithmForCustomers)

        };
        
        // Génération du token à partir du descripteur
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        // Ecrire le token au handler
        var jwt = tokenHandler.WriteToken(token);
        
        // Créer la réponse au client
        var response = new TokenGenerationResponseForCustomer
        {
            Customer = customerFound,
            AccessToken = jwt,
        };
        
        // Retourner la reponse
        return Ok(response);
    }
    
    // Méthode d'authentification des administrateurs, accessible par tout le monde
    [AllowAnonymous]
    [HttpPost("authenticateAdminAndGetAccessToken")]
    public async Task<IActionResult> GenerateTokenForAdmin([FromBody] TokenGenerationRequest tokenGenerationRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Il y'a une erreur dans la requête, réessayez plus tard");
        }
        var administratorFound = await _administratorRepository.Authenticate(tokenGenerationRequest);
        if (administratorFound is null) return NotFound("Le nom d'utilisateur ou mot de passe est incorrecte");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(StaticSecurityDatas.SecretKey);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Sub, administratorFound.Email),
            new (JwtRegisteredClaimNames.Email, administratorFound.Email),
            new ("userid", administratorFound.AdministratorId.ToString())
        };
        
        // On utilise un seul Claim pour toutes les méthodes des administrateurs
        var claim = new Claim(IdentityData.AdminPolicyName, IdentityData.AdminClaimValue, ClaimValueTypes.String);
        claims.Add(claim);
        
        // Le descripteur de jeton
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(StaticSecurityDatas.TokenLifeTime),
            Issuer = StaticSecurityDatas.Issuer,
            Audience = StaticSecurityDatas.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),StaticSecurityDatas.SecurityAlgorithmForAdministrators)
        };
        
        // Génération du token à partir du descripteur
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        // Ecrire le token au handler
        var jwt = tokenHandler.WriteToken(token);
        
        // Créer la réponse à l'administrateur
        var response = new TokenGenerationResponseForAdmin
        {
            Administrator = administratorFound,
            AccessToken = jwt,
        };
        
        // Retourner la reponse
        return Ok(response);
    }
}
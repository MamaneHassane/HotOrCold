using HotOrCold.Entities;
using HotOrCold.Security.Models;

namespace HotOrCold.Repositories.Interfaces;

public interface IAdministratorRepository
{
    Task<Administrator?> Register(Administrator administrator);
    Task<Administrator?> Authenticate(TokenGenerationRequest tokenGenerationRequest);
}
using HotOrCold.Datas;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using HotOrCold.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories.Implementations;

public class AdministratorRepository(ApplicationDbContext context) : IAdministratorRepository
{
    private readonly ApplicationDbContext _context = context;
    
    public async Task<Administrator?> Register(Administrator administrator)
    {
        await _context.Administrators.AddAsync(administrator);
        await _context.SaveChangesAsync();
        return administrator;
    }

    public async Task<Administrator?> Authenticate(TokenGenerationRequest tokenGenerationRequest)
    {
        var theAdministrator = await _context.Administrators.Where(
            administrator => administrator.Username == tokenGenerationRequest.Username
                             &&
                             administrator.Password == tokenGenerationRequest.Password
        ).FirstOrDefaultAsync();
        return theAdministrator;
    }
}
using HotOrCold.Datas;

namespace HotOrCold.Repositories;

public class CommandRepository
{
    private readonly ApplicationDbContext _context;

    public CommandRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
}
using HotOrCold.Datas;
using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Enumerations;
using HotOrCold.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories.Implementations;

public class CommandRepository(ApplicationDbContext context, ICartRepository cartRepository, ICustomerRepository customerRepository) : ICommandRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    public Command Create(Command command)
    {
        _context.Commands.Add(command);
        _context.SaveChanges();
        return command;
    }
    public Command? Create(CreateCommandDto createCommandDto)
    {
        var theCustomer = _context.Customers.Find(createCommandDto.CustomerId);
        if (theCustomer is null) return null;
        var theCommand = new Command
        {
            CustomerId = theCustomer.CustomerId,
            DrinkCopies = createCommandDto.DrinkCopies,
            CommandDate = DateOnly.FromDateTime(DateTime.Today)
        };
        _context.Commands.Add(theCommand);
        _context.SaveChanges();
        return theCommand;
    }
    public IEnumerable<Command>? GetActivesCommandByCustomerId(int customerId) 
    {
        var theCustomer = _context.Customers.Find(customerId);
        if(theCustomer is null) return null;
        IEnumerable<Command> theCommands = _context.Commands.AsNoTracking()
                                                            .Where(command => command.CustomerId == theCustomer.CustomerId)
                                                            .Where(command => command.CommandStatus.Equals(CommandStatus.OnGoing) )
                                                            .OrderBy(command => command.CommandDate)
                                                            .Take(20);
        return theCommands;
    }
    public IEnumerable<Command>? GetHistorizedCommandByCustomerId(int customerId) 
    {
        var theCustomer = _context.Customers.Find(customerId);
        if(theCustomer is null) return null;
        IEnumerable<Command> theCommands = _context.Commands.AsNoTracking()
                                                            .Where(command => command.CustomerId == theCustomer.CustomerId)
                                                            .Where(command => command.CommandStatus.Equals(CommandStatus.Done) )
                                                            .OrderBy(command => command.CommandDate)
                                                            .Take(20);
        return theCommands;
    }

    public bool DoCommandAndClearCart(DoCommandAndClearCartDto doCommandAndClearCartDto)
    {
        var theCustomer = _context.Customers.Find(doCommandAndClearCartDto.CustomerId);
        if (theCustomer is null) return false;    
        var theCommand = Create
        (
            new CreateCommandDto(doCommandAndClearCartDto.DrinkCopies ,doCommandAndClearCartDto.CustomerId)                 
        );
        if (theCommand is null) return false;
        
        // Le client paye
        _customerRepository.DecreaseBalance(theCustomer.CustomerId,theCommand.Price);

        _cartRepository.ClearCart(doCommandAndClearCartDto.CartId);
        return true;
    }

    public Command Create(ICollection<DrinkCopy> drinkCopies, int customerId)
    {
        throw new NotImplementedException();
    }
}

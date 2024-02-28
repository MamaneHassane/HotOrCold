using HotOrCold.Datas;
using HotOrCold.Dtos;
using HotOrCold.Entities;
using HotOrCold.Enumerations;
using HotOrCold.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories;

public class CommandRepository(ApplicationDbContext context, ICartRepository cartRepository) : ICommandRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ICartRepository _cartRepository = cartRepository;

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
            Customer = theCustomer,
            DrinkCopies = createCommandDto.DrinkCopies
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
                                                            .Where(command => command.Customer.CustomerId == theCustomer.CustomerId)
                                                            .Where(command => command.CommandStatus.Equals(CommandStatus.OnGoing) );
        return theCommands;
    }
    public IEnumerable<Command>? GetHistorizedCommandByCustomerId(int customerId) 
    {
        var theCustomer = _context.Customers.Find(customerId);
        if(theCustomer is null) return null;
        IEnumerable<Command> theCommands = _context.Commands.AsNoTracking()
                                                            .Where(command => command.Customer.CustomerId == theCustomer.CustomerId)
                                                            .Where(command => command.CommandStatus.Equals(CommandStatus.Done) );
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
        _cartRepository.ClearCart(doCommandAndClearCartDto.CartId);
        return true;
    }

    public Command Create(ICollection<DrinkCopy> drinkCopies, int customerId)
    {
        throw new NotImplementedException();
    }
}

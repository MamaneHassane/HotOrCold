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

    private Command? Create(CreateCommandDto createCommandDto)
    {
        var theCustomer = _context.Customers.Find(createCommandDto.CustomerId);
        if (theCustomer is null) return null;
        var theCommand = new Command
        {
            CustomerId = theCustomer.CustomerId,
            DrinkCopies = createCommandDto.DrinkCopies,
            CommandDate = DateOnly.FromDateTime(DateTime.Today),
            Price = createCommandDto.Price
        };
        _context.Commands.Add(theCommand);
        _context.SaveChanges();
        return theCommand;
    }
    
    public async Task<IEnumerable<Command?>?> GetAllCommands()
    {
        return await _context.Commands.ToListAsync();
    }
    
    public async Task<Command?> Create(Command command)
    {
        await _context.Commands.AddAsync(command);
        await _context.SaveChangesAsync();
        return command;
    }
    
    public async Task<Command?> Create(IEnumerable<DrinkCopy> drinkCopies, int customerId)
    {
        var command = new Command
        {
            CustomerId = customerId,
            CommandDate = DateOnly.FromDateTime(DateTime.Today),
            CommandStatus = CommandStatus.OnGoing,
            DrinkCopies = drinkCopies,
        };
        await _context.Commands.AddAsync(command);
        return command;
    }

    public async Task<Command?> Get(int id)
    {
        return await _context.Commands.FindAsync(id);
    }
    
    public async Task<IEnumerable<Command?>> GetActivesCommandByCustomerId(int customerId) 
    {
        var theCustomer = await _context.Customers.FindAsync(customerId);
        if(theCustomer is null) return new List<Command>();
        IEnumerable<Command> theCommands = await _context.Commands
            .AsNoTracking()
            .Where(command => command.CustomerId == theCustomer.CustomerId)
            .Where(command => command.CommandStatus.Equals(CommandStatus.OnGoing))
            .OrderByDescending(command => command.CommandDate)
            .ToListAsync();
        return theCommands;
    }
    
    public async Task<IEnumerable<Command?>> GetHistorizedCommandByCustomerId(int customerId) 
    {
        var theCustomer = await _context.Customers.FindAsync(customerId);
        if(theCustomer is null) return new List<Command>();
        IEnumerable<Command> theCommands = _context.Commands.AsNoTracking()
                                                            .Where(command => command.CustomerId == theCustomer.CustomerId)
                                                            .Where(command => command.CommandStatus.Equals(CommandStatus.Done) )
                                                            .OrderByDescending(command => command.CommandDate)
                                                            .Take(20);
        return theCommands;
    }

    public async Task<bool> DoCommandAndClearCart(DoCommandAndClearCartDto doCommandAndClearCartDto)
    {
        var theCustomer = await _context.Customers.FindAsync(doCommandAndClearCartDto.CustomerId);
        if (theCustomer is null) return false;
        var theCart = await _context.Carts.Where(cart=>cart.CartId==theCustomer.CartId).Include(cart=>cart.DrinkCopies).FirstOrDefaultAsync();
        if (theCart?.DrinkCopies is null) return false;
        double price = 0;
        var theDrinkCopies = new List<DrinkCopy>();
        foreach (var theDrinkCopy in theCart.DrinkCopies)
        {
            theDrinkCopies.Add(theDrinkCopy);
            price += theDrinkCopy.Price;
        }
        var theCommand = Create
        (
            new CreateCommandDto(theDrinkCopies ,doCommandAndClearCartDto.CustomerId, price)                 
        );
        if (theCommand is null || theCommand.Price>theCustomer.Balance) return false;
        // Le client paye
        await _customerRepository.DecreaseBalance(theCustomer.CustomerId,theCommand.Price);
        await _cartRepository.ClearCart(doCommandAndClearCartDto.CartId);
        return true;
    }

    public async Task<bool> ConfirmCommandDeliveredAndPayed(ConfirmCommandDeliveredAndPayedDto confirmCommandDeliveredAndPayedDto)
    {
        var theCommand = await _context.Commands.FindAsync(confirmCommandDeliveredAndPayedDto.CommandId);
        if (theCommand is null || theCommand.CommandStatus == CommandStatus.Done) return false;
        var theCustomer = await _context.Customers.FindAsync(confirmCommandDeliveredAndPayedDto.CustomerId);
        if (theCustomer is null) return false;
        // La commande est archivée
        theCommand.CommandStatus = CommandStatus.Done;
        _context.Commands.Update(theCommand);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> CancelCommand(CancelCommandDto cancelCommandDto)
    {
        var theCommand = await _context.Commands.Where(command=>command.CommandId==cancelCommandDto.CommandId)
            .Include(command=>command.DrinkCopies)
            .FirstOrDefaultAsync();
        if (theCommand is null || theCommand.CommandStatus == CommandStatus.Done) return false;
        var theCustomer = await _context.Customers.FindAsync(cancelCommandDto.CustomerId);
        if (theCustomer is null) return false;
        // On supprime toutes les copies à livrer
        foreach (var theDrinkCopy in theCommand.DrinkCopies)
        {
            await _context.DrinkCopies.Where(drinkCopy=>drinkCopy.DrinkCopyId==theDrinkCopy.DrinkCopyId).ExecuteDeleteAsync();
        }
        // On rembourse le client
        await _customerRepository.IncreaseBalance(theCustomer.CustomerId, theCommand.Price);
        // On supprime la commande
        await _context.Commands.Where(command=>command.CommandId==cancelCommandDto.CommandId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
        return true;
    }
    
}
 
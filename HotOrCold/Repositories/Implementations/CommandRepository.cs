using HotOrCold.Datas;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HotOrCold.Repositories;

public class CommandRepository : ICommandRepository
{
    private readonly ApplicationDbContext _context;
    private readonly CartRepository _cartRepository;
    public CommandRepository(ApplicationDbContext context, CartRepository cartRepository)
    {
        _context = context;
        _cartRepository = cartRepository;
    }

    public void Create(Command command)
    {
        _context.Commands.Add(command);
        _context.SaveChanges();
    }
    public Command? Create(ICollection<DrinkCopy> drinkCopies, int customerId)
    {
        var theCustomer = _context.Customers.Find(customerId);
        if (theCustomer is null) return null;
        var theCommand = new Command
        {
            Customer = theCustomer,
            DrinkCopies = drinkCopies
        };
        _context.Commands.Add(theCommand);
        _context.SaveChanges();
        return theCommand;
    }

    public bool DoCommandAndClearCart(ICollection<DrinkCopy> drinkCopies, int customerId, int cartId)
    {
        var theCustomer = _context.Customers.Find(customerId);
        if (theCustomer is null) return false;
        var theCommand = this.Create(drinkCopies, customerId);
        if (theCommand is null) return false;
        _cartRepository.ClearCart(cartId);
        return true;
    }


}

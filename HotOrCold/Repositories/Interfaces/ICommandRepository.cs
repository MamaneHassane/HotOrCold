using HotOrCold.Dtos;
using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface ICommandRepository
{
    Command Create(Command command);
    Command Create(ICollection<DrinkCopy> drinkCopies, int customerId);
    IEnumerable<Command>? GetActivesCommandByCustomerId(int customerId);
    public IEnumerable<Command>? GetHistorizedCommandByCustomerId(int customerId);
    bool DoCommandAndClearCart(DoCommandAndClearCartDto doCommandAndClearCartDto);
}
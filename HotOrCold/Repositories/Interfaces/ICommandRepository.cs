using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface ICommandRepository
{
    void Create(Command command);
    Command? Create(ICollection<DrinkCopy> drinkCopies, int customerId);
    bool DoCommandAndClearCart(ICollection<DrinkCopy> drinkCopies, int customerId, int cartId);

}
using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface ICommandRepository
{
    Task<IEnumerable<Command?>?> GetAllCommands();
    Task<Command?> Create(Command command);
    Task<Command?> Get(int id);
    Task<Command?> Create(IEnumerable<DrinkCopy> drinkCopies, int customerId);
    Task<IEnumerable<Command?>> GetActivesCommandByCustomerId(int customerId);
    Task<IEnumerable<Command?>> GetHistorizedCommandByCustomerId(int customerId);
    Task<bool> DoCommandAndClearCart(DoCommandAndClearCartDto doCommandAndClearCartDto);
    Task<bool> CancelCommand(CancelCommandDto cancelCommandDto);
    Task<bool> ConfirmCommandDeliveredAndPayed(ConfirmCommandDeliveredAndPayedDto confirmCommandDeliveredAndPayedDto);
}
using HotOrCold.Entities;

namespace HotOrCold.Dtos.Definitions;

public class CreateCommandDto
{
    public IEnumerable<DrinkCopy> DrinkCopies { get; set; }
    public int CustomerId { get; set; }
    public CreateCommandDto(IEnumerable<DrinkCopy> drinkCopies, int customerId)
    {
        DrinkCopies = drinkCopies;
        CustomerId = customerId;
    }
} 

public class DoCommandAndClearCartDto
{
    public IEnumerable<DrinkCopy> DrinkCopies { get; set; }
    public int CustomerId { get; set; }
    public int CartId { get; set; }
}

public class ConfirmCommandDeliveredAndPayDto
{
    public int CommandId { get; set; }
    public int CustomerId { get; set; }
}

public class CancelCommandDto
{
    public int CommandId { get; set; }
    public int CustomerId { get; set; }
}
   


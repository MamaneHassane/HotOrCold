using HotOrCold.Entities;

namespace HotOrCold.Dtos.Definitions;

public class CreateCommandDto(IEnumerable<DrinkCopy> drinkCopies, int customerId, double price)
{
    public IEnumerable<DrinkCopy> DrinkCopies { get; set; } = drinkCopies;
    public int CustomerId { get; set; } = customerId;

    public double Price { get; set; } = price;
} 

public class DoCommandAndClearCartDto
{
    public IEnumerable<int> DrinkCopiesIds { get; set; }
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
   


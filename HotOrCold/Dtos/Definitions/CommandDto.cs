using HotOrCold.Entities;
namespace HotOrCold.Dtos;

public record CreateCommandDto
{
    public IEnumerable<DrinkCopy> DrinkCopies { get; }
    public int CustomerId { get; }
    public CreateCommandDto(IEnumerable<DrinkCopy> drinkCopies, int customerId)
    {
        DrinkCopies = drinkCopies;
        CustomerId = customerId;
    }
} 

public record DoCommandAndClearCartDto (
    IEnumerable<DrinkCopy> DrinkCopies,
    int CustomerId,
    int CartId
);

public record ReadCommandDto (
    // Pas besoin pour l'instant
);

public record UpdateCommandDto (
    // Pas besoin pour l'instant
);
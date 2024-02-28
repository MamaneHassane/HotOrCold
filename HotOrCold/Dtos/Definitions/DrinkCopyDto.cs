namespace HotOrCold.Dtos;

public record AddDrinkCopyDto(
    int CartId, 
    int DrinkId, 
    int QuantityInLiter
);

public record AddManyDrinkCopiesDto(
    int CartId,
    IEnumerable<AddDrinkCopyDto> AddDrinkCopyDtoCollection
);
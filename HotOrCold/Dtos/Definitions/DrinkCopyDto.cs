namespace HotOrCold.Dtos.Definitions;

public class AddDrinkCopyDto
{
    public int CartId { get; set; }
    public int DrinkId { get; set; }
    public double QuantityInLiter { get; set; }
};

public class AddManyDrinkCopiesDto
{
    public int CartId { get; set; }
    public IEnumerable<AddDrinkCopyDto> AddDrinkCopyDtoCollection { get; set; }
}

public class RemoveDrinkCopyFromCartDto
{
    public int CartId { get; set; }
    public int DrinkCopyId { get; set; }
}

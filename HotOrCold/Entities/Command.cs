using HotOrCold.Enumerations;

namespace HotOrCold.Entities;

public class Command
{
    public int CommandId { get; set; }
    // Une Command correspond à un seul Customer
    public required Customer Customer { get; set; }

    // Une commande à un status
    public CommandStatus CommandStatus { get; set; } 
    // Une Command contient une liste de DrinkCopy
    public IEnumerable<DrinkCopy>? DrinkCopies { get; set; }
    
    // Une Command à un prix dérivé
    public double Price
    {
        get
        {
            double price = 0;
            if(DrinkCopies is null) return price;
            foreach(DrinkCopy drinkCopy in DrinkCopies)
            {
                price += drinkCopy.Price;
            }
            return price;
        }
    }
}
namespace HotOrCold.Entities;

public class Command
{
    public int CommandId { get; set; }
    // Une Command correspond à un seul Customer
    public Customer Customer { get; set; }
    
    // Une Command contient une liste de DrinkCopy
    public ICollection<DrinkCopy> DrinkCopies { get; set; }
    
    // Une Command à un prix dérivé
    public double Price
    {
        get
        {
            double price = 0;
            foreach(DrinkCopy drinkCopy in this.DrinkCopies)
            {
                price += drinkCopy.Price;
            }

            return price;
        }
    }
}
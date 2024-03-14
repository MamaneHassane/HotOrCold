using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotOrCold.Enumerations;

namespace HotOrCold.Entities;

public class Command
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommandId { get; set; }
    // Une commande correspond à un seul Customer
    [ForeignKey("customer_id")]
    public int CustomerId { get; set; }
    // Une commande à une date
    public required DateOnly CommandDate { get; set; }
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
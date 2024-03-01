using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotOrCold.Enumerations;

namespace HotOrCold.Entities;

public class Drink
{
    // L'identifiant de la boisson
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DrinkId { get; set; }
    
    // Le type de la boisson
    [Range(0,6)]
    public int Drinktype { get; set; }
    
    // Le nom de la boisson
    public string DrinkName
    {
        get 
        {
            switch (Drinktype)
            {
                case 0 : return DrinkType.BlackCoffee.ToString();
                case 1 : return DrinkType.LongCoffee.ToString();
                case 2 : return DrinkType.Expresso.ToString();
                case 4 : return DrinkType.Tea.ToString();
                case 5 : return DrinkType.Milk.ToString();
                case 6 : return DrinkType.CoffeeMilk.ToString(); 
                case 7 : return DrinkType.MachaLatte.ToString(); 
            }
            return DrinkType.Unclassified.ToString();
        }
    }

    // Le prix par litre de la boisson
    [Range(1, 5)] 
    public double PricePerLiter { get; set; }
    
    // L'image de la boisson
    public string? ImageUrl { get; set; }
    
    // Propriété de navigation OneToMany : Un Drink possède plusieurs DrinkCopy
    public ICollection<DrinkCopy>? DrinkCopies { get; set; }
    
    // Propriété de navigation ManyToMany : Un Drink possède plusieurs Category
    public ICollection<Category>? Categories { get; set; }
    
}
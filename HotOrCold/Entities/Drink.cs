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
    [Range(0,7)]
    public int Drinktype { get; set; }
    // Le nom de la boisson
    public string DrinkName 
    {
        get
        {
            return Drinktype switch
            {
                0 => DrinkType.BlackCoffee.ToString(),
                1 => DrinkType.LongCoffee.ToString(),
                2 => DrinkType.Expresso.ToString(),
                4 => DrinkType.Tea.ToString(),
                5 => DrinkType.Milk.ToString(),
                6 => DrinkType.CoffeeMilk.ToString(),
                7 => DrinkType.MachaLatte.ToString(),
                _ => DrinkType.Unclassified.ToString()
            };
        }
    }

    // Le prix par litre de la boisson
    [Range(1, 5)] 
    public double PricePerLiter { get; set; }
    
    // L'image de la boisson
    public string? ImageUrl { get; set; }
    
    // Propriété de navigation OneToMany : Un Drink possède plusieurs DrinkCopy
    public ICollection<DrinkCopy>? DrinkCopies { get; set; } = new List<DrinkCopy>();
    
    // Propriété de navigation ManyToMany : Un Drink possède plusieurs Category
    public ICollection<Category>? Categories { get; set; } = new List<Category>();

}
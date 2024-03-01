using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotOrCold.Entities;

public class DrinkCopy
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // L'identifiant de l'exemplaire
    public int DrinkCopyId { get; set; }
    
    // Une DrinkCopy correspond à un seul Drink
    // Propriété de navigation inverse ManyToOne
    public Drink? Drink { get; set; }
    
    // Un client ne peut pas commander moins de 0.5 litres et plus de 1 litre de boisson
    [Range(0.5,1)]
    public double QuantityInLiter { get; set; }
    
    // Le prix de l'exemplaire qui est dérivé
    public double Price 
    {
        get
        {
            return
            Drink == null ?  0 
            :
            Drink.PricePerLiter * QuantityInLiter;
        }
    }  
    // Une DrinkCopy appartient à un seul Cart
    // La clé étrangère est toujours du côté Many, elle n'est pas nécéssaire
    // Propriéte de navigation inverse ManyToOne
    public Cart? Cart { get; set; }
}
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
    public int DrinkId { get; set; }
    // Un client ne peut pas commander moins de 0.5 litres et plus de 1 litre de boisson
    [Range(0.5,1)]
    public double QuantityInLiter { get; set; }
    // Le prix de l'exemplaire qui sera dérivé
    public double Price { get; set; }
    // Une DrinkCopy appartient à un seul Cart
    public int? CartId { get; set; }
}
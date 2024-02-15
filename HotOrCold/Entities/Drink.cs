using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using HotOrCold.Enumerations;

namespace HotOrCold.Entities;

public class Drink
{
    // L'identifiant de la boisson
    [Required(ErrorMessage = "L'identifiant de la boisson est manquant")]
    public int DrinkId { get; set; }
    
    // Le type de la boisson
    [Required] 
    [StringLength(11,ErrorMessage = "Le nom de la boisson est manquant",MinimumLength = 1)]
    public DrinkType Name { get; set; }
    
    // Le prix par litre de la boisson
    [Range(1, 5)] 
    public double PricePerLiter { get; set; }
    
    // L'image de la boisson
    public byte[] Image { get; set; }
    
    // Propriété de navigation OneToMany : Un Drink possède plusieurs DrinkCopy
    public ICollection<DrinkCopy> DrinkCopies { get; set; }
    
    // Propriété de navigation ManyToMany : Un Drink possède plusieurs Category
    public ICollection<Category> Categories { get; set; }
    

}
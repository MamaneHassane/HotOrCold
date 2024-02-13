using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using HotOrCold.Enumerations;

namespace HotOrCold.Entities;

public class Drink
{
    [Required(ErrorMessage = "L'identifiant de la boisson est manquant")]
    private int Id { get; set; }
    
    [Required] 
    [StringLength(11,ErrorMessage = "Le nom de la boisson est manquant",MinimumLength = 1)]
    private DrinkType Name { get; set; }
    
    [Range(1, 5)] 
    
    private Double Price { get; set; }

    private byte[] Image { get; set; }

}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HotOrCold.Entities;

public class Cart
{  
     // L'identifiant du Cart
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public int CartId { get; set; }
     // Un Cart contient plusieurs DrinkCopy
     // Propriéte de navigation OneToMany
     public ICollection<DrinkCopy>? DrinkCopies { get; set; } = new List<DrinkCopy>();
}
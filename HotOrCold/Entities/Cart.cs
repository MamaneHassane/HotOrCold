using System.ComponentModel.DataAnnotations.Schema;

namespace HotOrCold.Entities;

public class Cart
{  
     // L'identifiant du Cart
     public int CartId { get; set; }
     // Un Cart appartient à un seul Customer
     // Clé étrangère du Customer
     [ForeignKey(("Customer"))]
     public int CustomerId { get; set; }
     // Propriété de navigation inverse OneToOne : pas nécéssaire mais bonne pratique
     public Customer Customer { get; set; }
     
     // Un Cart contient plusieurs DrinkCopy
     // Propriéte de navigation OneToMany
     public ICollection<DrinkCopy> DrinkCopies { get; set; }
     
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotOrCold.Enumerations;

namespace HotOrCold.Entities;

public class Category
{
    // L'identifiant de la catégorie
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }
    // Le nom de la catégorie
    public CategoryName CategoryName { get; set; }
    // Propriété de navigation ManyToMany : Une Category contient plusieurs Drink
    public ICollection<Drink>? Drinks { get; set; }
}
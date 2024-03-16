using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotOrCold.Entities;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    
    [Required] [StringLength(100, ErrorMessage = "Essayer s'il vous plaît de raccourcir le nom")]
    public required string Lastname { get; set; }

    [Required] [StringLength(100, ErrorMessage = "Essayer s'il vous plaît de raccourcir le prénom")]
    public required string Firstname { get; set; }
    
    [Required] 
    [StringLength(30,ErrorMessage = "Essayer s'il vous plaît de raccourcir le nom d'utilisateur")]
    public required string Username { get; set; }
    
    [Required]
    [StringLength(20,ErrorMessage = "Le mot de passe doit avoir entre 8 et 20 caractères",MinimumLength = 8)]
    public required string Password { get; set; }
    
    [Required] 
    [EmailAddress(ErrorMessage = "L'email n'est pas correcte")]
    public required string Email { get; set; }

    [Required] 
    public double Balance { get; set; }

    [Required] 
    public DateOnly DateOfBirth { get; set; }
    // Propriéte de navigation OneToOne : Un Customer possède un Cart
    public int CartId { get; set; }
    // Propriété de navigation OneToMany : Un Customer passe plusieurs Command
    public IEnumerable<Command>? Commands { get; set; } = new List<Command>();

}
using System.ComponentModel.DataAnnotations;

namespace HotOrCold.Entities;

public class Customer
{
    public int CustomerId { get; set; }

    [Required] [StringLength(100, ErrorMessage = "Essayer s'il vous plaît de raccourcir le nom")]
    public required String Lastname { get; set; }

    [Required] [StringLength(100, ErrorMessage = "Essayer s'il vous plaît de raccourcir le prénom")]
    public required String Firstname { get; set; }
    
    [Required] 
    [StringLength(30,ErrorMessage = "Essayer s'il vous plaît de raccourcir le nom d'utilisateur")]
    public required String Username { get; set; }
    
    [Required]
    [StringLength(20,ErrorMessage = "Le mot de passe doit avoir entre 8 et 20 caractères",MinimumLength = 8)]
    public required String Password { get; set; }
    
    [Required] 
    [EmailAddress(ErrorMessage = "L'email n'est pas correcte")]
    public required String Email { get; set; }

    [Required] 
    public double Balance { get; set; }

    [Required] 
    public DateOnly DateOfBirth { get; set; }
    // Propriéte de navigation OneToOne : Un Customer possède un Cart
    public required Cart Cart { get; set; }
    // Propriété de navigation OneToMany : Un Customer passe plusieurs Command
    public required IEnumerable<Command> Commands { get; set; }
    
}
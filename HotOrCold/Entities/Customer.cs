using System.ComponentModel.DataAnnotations;

namespace HotOrCold.Entities;

public class Customer
{
    public int CustomerId { get; set; }

    [Required] [StringLength(100, ErrorMessage = "Essayer s'il vous plaît de raccourcir le nom")]
    public String Lastname { get; set; }

    [Required] [StringLength(100, ErrorMessage = "Essayer s'il vous plaît de raccourcir le prénom")]
    public String Firstname { get; set; }
    
    [Required] 
    [StringLength(30,ErrorMessage = "Essayer s'il vous plaît de raccourcir le nom d'utilisateur")]
    public String Username { get; set; }
    
    [Required]
    [StringLength(20,ErrorMessage = "Le mot de passe doit avoir entre 8 et 20 caractères",MinimumLength = 8)]
    public String Password { get; set; }
    
    [Required] [EmailAddress] private String Email { get; set; }
    
    [Required] public double Balance { get; set; }
    
    [Required] public DateOnly DateOfBirth { get; set; }
    
    // Propriéte de navigation OneToOne : Un Customer possède un Cart
    public Cart Cart { get; set; }
    
    // Propriété de navigation OneToMany : Un Customer passe plusieurs Command
    public ICollection<Command> Commands { get; set; }
    
}
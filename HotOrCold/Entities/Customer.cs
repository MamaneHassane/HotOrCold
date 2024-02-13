using System.ComponentModel.DataAnnotations;

namespace HotOrCold.Entities;

public class Customer
{
    private int Id { get; set; }

    [Required] [StringLength(100, ErrorMessage = "Essayer s'il vous plaît de raccourcir le nom")]
    private String Lastname;

    [Required] [StringLength(100, ErrorMessage = "Essayer s'il vous plaît de raccourcir le prénom")]
    private String Firstname;
    
    

}
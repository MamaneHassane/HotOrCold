using HotOrCold.Entities;

namespace HotOrCold.Security.Models;

public class TokenGenerationResponseForCustomer
{
    public Customer Customer { get; set; }
    
    public string AccessToken { get; set; }
}
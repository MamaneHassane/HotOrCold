using HotOrCold.Entities;
using HotOrCold.Security.Models;

namespace HotOrCold.Security.Identity;

public class IdentityData
{
    // Pour le customer
    public const string CustomerPolicyName = "Customer";
    public const string CustomerClaimValue = "true";
    
    // Pour les administrateurs
    public const string AdminPolicyName = "Admin";
    public const string AdminClaimValue = "true";
}
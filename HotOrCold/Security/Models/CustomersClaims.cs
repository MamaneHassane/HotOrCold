using System.Runtime.Serialization;

namespace HotOrCold.Security.Models;

public enum CustomersClaims
{
    // On peut séparer en plusieurs, mais c'est pas nécéssaire
    [EnumMember(Value = "Customer")]
    Customer,
}
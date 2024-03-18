using System.Runtime.Serialization;

namespace HotOrCold.Security.Models;

public enum AdminClaims
{
    [EnumMember(Value = "Admin")]
    Admin,
}
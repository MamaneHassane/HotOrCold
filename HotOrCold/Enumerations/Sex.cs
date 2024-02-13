using System.Runtime.Serialization;

namespace HotOrCold.Enumerations;

public enum Sex
{
    [EnumMember(Value = "Male")]
    Male,
    [EnumMember(Value = "Female")]
    Female,
    [EnumMember(Value = "Lbgt")]
    Lgbt,
}
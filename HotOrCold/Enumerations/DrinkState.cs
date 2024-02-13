using System.Runtime.Serialization;

namespace HotOrCold.Enumerations;

public enum DrinkState
{
    [EnumMember(Value = "Hot")]
    Hot, // Chaud
    [EnumMember(Value = "Lukewarm")]
    Lukewarm, // Tiède
    [EnumMember(Value = "Cold")]
    Cold, // Froid
}
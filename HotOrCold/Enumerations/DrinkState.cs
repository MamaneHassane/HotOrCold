using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotOrCold.Enumerations;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DrinkState
{
    [EnumMember(Value = "Hot")]
    Hot, // Chaud
    [EnumMember(Value = "Lukewarm")]
    Lukewarm, // Tiède
    [EnumMember(Value = "Cold")]
    Cold, // Froid
}
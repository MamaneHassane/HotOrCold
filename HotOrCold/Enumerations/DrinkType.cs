using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotOrCold.Enumerations;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DrinkType
{
    [EnumMember(Value = "BlackCoffee")]
    BlackCoffee,
    [EnumMember(Value= "LongCoffee")]
    LongCoffee,
    [EnumMember(Value = "Expresso")]
    Expresso,
    [EnumMember(Value = "Tea")]
    Tea,
    [EnumMember(Value = "Milk")]
    Milk,
    [EnumMember(Value = "CoffeeMilk")]
    CoffeeMilk,
    [EnumMember(Value = "MachaLatte")]
    MachaLatte,
    [EnumMember(Value = "Unclassified")]
    Unclassified,
}
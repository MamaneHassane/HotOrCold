using System.Runtime.Serialization;

namespace HotOrCold.Enumerations;

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
}
using System.Runtime.Serialization;

namespace HotOrCold.Enumerations;

public enum CategoryName
{
    [EnumMember(Value = "Casual")]
    Casual,
    [EnumMember(Value = "Nutritive")]
    Nutritive,
    [EnumMember(Value = "Health")]
    Health,
    [EnumMember(Value = "Diet")]
    Diet,
    [EnumMember(Value = "Weight Gain")]
    WeigthGain,
}
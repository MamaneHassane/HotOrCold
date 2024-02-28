using System.Runtime.Serialization;

namespace HotOrCold.Enumerations;

public enum CommandStatus
{
    [EnumMember(Value = "OnGoing")]
    OnGoing,
    [EnumMember(Value = "Done")]
    Done,
}
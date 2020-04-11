using System.Runtime.Serialization;

namespace QLector.Domain.Core
{
    public enum MessageType : byte
    {
        [EnumMember(Value = "error")]
        Error = 0,

        [EnumMember(Value = "validationError")]
        ValidationError = 5,

        [EnumMember(Value = "info")]
        Information = 10
    }
}
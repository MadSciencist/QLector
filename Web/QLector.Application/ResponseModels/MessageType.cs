using System.Runtime.Serialization;

namespace QLector.Application.ResponseModels
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
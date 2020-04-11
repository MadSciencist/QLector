using System.Collections.Generic;

namespace QLector.Domain.Core
{
    public class GenericResponse<TDto>
    {
        public TDto Dto { get; }
        public List<Message> Messages { get; }
        public bool IsSuccess { get; }

        public GenericResponse(TDto dto, bool isSuccess = true)
        {
            Dto = dto;
            IsSuccess = isSuccess;
            Messages = new List<Message>();
        }

        public GenericResponse<TDto> AddInfoMessage(string message)
        {
            Messages.Add(new Message { Type = MessageType.Information, Value = message });
            return this;

        }
        public GenericResponse<TDto> AddErrorMessage(string message)
        {
            Messages.Add(new Message {Type = MessageType.Error, Value = message});
            return this;
        }
    }
}

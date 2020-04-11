namespace QLector.Domain.Core
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string Value { get; set; }

        public static Message Error(string message)
        {
            return new Message
            {
                Type = MessageType.Error,
                Value = message
            };
        }

        public static Message Info(string message)
        {
            return new Message
            {
                Type = MessageType.Information,
                Value = message
            };
        }
    }
}
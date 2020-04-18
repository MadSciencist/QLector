namespace QLector.Domain.Core
{
    /// <summary>
    /// Provides notification to end user
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Type of the message
        /// </summary>
        public MessageType Type { get; set; }

        /// <summary>
        /// Message itself
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Factory method for creating new instance
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Message Error(string message)
        {
            return new Message
            {
                Type = MessageType.Error,
                Value = message
            };
        }

        /// <summary>
        /// Factory method for creating new instance
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
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
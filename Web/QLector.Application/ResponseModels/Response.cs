using Newtonsoft.Json;
using System.Collections.Generic;

namespace QLector.Application.ResponseModels
{
    public class Response<TData>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public TData Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<Message> Messages { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object ProblemDetails { get; set; }

        [JsonIgnore]
        public int? ResponseStatusCodeOverride { get; set; }

        public Response<TData> AddError(string error)
        {
            Messages.Add(new Message
            {
                Type = MessageType.Error,
                Value = error
            });

            return this;
        }
        public static Response<TData> FromError(string error, object problemDetails = null)
        {
            return new Response<TData>
            {
                ProblemDetails = problemDetails,
                Messages = new List<Message>
                {
                    new Message
                    {
                        Type = MessageType.Error,
                        Value = error
                    }
                }
            };
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace QLector.Application.ResponseModels
{
    /// <summary>
    /// Application layer response container
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class Response<TResponse>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TResponse Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<Message> Messages { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object ProblemDetails { get; set; }

        [JsonIgnore]
        public int? ResponseStatusCodeOverride { get; private set; }

        public Response()
        {
            Messages = new List<Message>();
        }

        public Response<TResponse> SetStatusCodeOverride(int statusCode)
        {
            if(statusCode >= 100 && statusCode <= 599)
            {
                ResponseStatusCodeOverride = statusCode;
                return this;
            }

            throw new InvalidOperationException("Status code must be within 100-599 range");
        }

        public Response<TResponse> AddError(string error)
        {
            Messages.Add(new Message
            {
                Type = MessageType.Error,
                Value = error
            });

            return this;
        }

        public Response<TResponse> AddInformation(string message)
        {
            Messages.Add(new Message
            {
                Type = MessageType.Information,
                Value = message
            });

            return this;
        }

        public static Response<TResponse> FromError(string error, object problemDetails = null)
        {
            return new Response<TResponse>
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

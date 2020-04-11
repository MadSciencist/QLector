using Newtonsoft.Json;
using QLector.Domain.Core;
using System;
using System.Collections.Generic;

namespace QLector.Application.Core
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
        public List<Message> Messages { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object ProblemDetails { get; set; }

        [JsonIgnore]
        private int? _responseStatusCodeOverride;

        public Response()
        {
            Messages = new List<Message>();
        }

        public int? GetResponseStatusCodeOverride() => _responseStatusCodeOverride;

        public Response<TResponse> SetStatusCodeOverride(int statusCode)
        {
            if(statusCode >= 100 && statusCode <= 599)
            {
                _responseStatusCodeOverride = statusCode;
                return this;
            }

            throw new InvalidOperationException("Status code must be within 100-599 range");
        }

        public Response<TResponse> AddMessages(IEnumerable<Message> messages)
        {
            Messages.AddRange(messages);
            return this;
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

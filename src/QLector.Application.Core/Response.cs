﻿using Newtonsoft.Json;
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
        /// <summary>
        /// Data Transfer Object
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public TResponse Data { get; set; }

        /// <summary>
        /// List of messages generated by domain layer
        /// </summary>
        [JsonProperty("messages", NullValueHandling = NullValueHandling.Ignore)]
        public List<Message> Messages { get; set; }

        /// <summary>
        /// Details of encountered issue
        /// </summary>
        [JsonProperty("problemDetails", NullValueHandling = NullValueHandling.Ignore)]
        public object ProblemDetails { get; set; }

        /// <summary>
        /// Used to set override of HTTP response code
        /// </summary>
        [JsonIgnore]
        private int? _responseStatusCodeOverride;

        public Response()
        {
            Messages = new List<Message>();
        }

        public int? GetResponseStatusCodeOverride() => _responseStatusCodeOverride;

        /// <summary>
        /// Sets HTTP code override
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public Response<TResponse> SetStatusCodeOverride(int statusCode)
        {
            if(statusCode >= 100 && statusCode <= 599)
            {
                _responseStatusCodeOverride = statusCode;
                return this;
            }

            throw new InvalidOperationException("Status code must be within 100-599 range");
        }

        /// <summary>
        /// Add a range of domain messages
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public Response<TResponse> AddMessages(IEnumerable<Message> messages)
        {
            Messages.AddRange(messages);
            return this;
        }

        /// <summary>
        /// Add an error message
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public Response<TResponse> AddError(string error)
        {
            Messages.Add(new Message
            {
                Type = MessageType.Error,
                Value = error
            });

            return this;
        }

        /// <summary>
        /// Add an information message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Response<TResponse> AddInformation(string message)
        {
            Messages.Add(new Message
            {
                Type = MessageType.Information,
                Value = message
            });

            return this;
        }

        /// <summary>
        /// Add an validation message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Response<TResponse> AddValidationMessage(string message)
        {
            Messages.Add(new Message
            {
                Type = MessageType.ValidationError,
                Value = message
            });

            return this;
        }

        /// <summary>
        /// Creates new Response object
        /// </summary>
        /// <param name="error"></param>
        /// <param name="problemDetails"></param>
        /// <returns></returns>
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

using AutoMapper;
using Microsoft.Extensions.Logging;
using System;

namespace QLector.Application.Handlers
{
    public abstract class BaseHandler
    {
        protected IMapper Mapper { get; }
        protected ILogger<BaseHandler> Logger { get; }

        public BaseHandler(IMapper mapper, ILogger<BaseHandler> logger)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}

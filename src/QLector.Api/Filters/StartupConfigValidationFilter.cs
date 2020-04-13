using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using QLector.Domain.Core;
using System;
using System.Collections.Generic;

namespace QLector.Api.Filters
{
    public class StartupConfigValidationFilter : IStartupFilter
    {
        private readonly IEnumerable<IValidatable> _validatables;

        public StartupConfigValidationFilter(IEnumerable<IValidatable> validatables)
        {
            _validatables = validatables;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (var validatable in _validatables)
            {
                // Will throw if invalid
                validatable.Validate();
            }

            return next;
        }
    }
}

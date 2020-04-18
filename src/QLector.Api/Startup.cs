using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using QLector.Api.Extensions;
using QLector.Api.Filters;
using QLector.Application.Core.Extensions;
using QLector.DAL.EF;
using QLector.Security;

namespace QLector.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers(options => options.Filters.Add(new ErrorHandlingFilter()))
                .AddNewtonsoftJson(o => o.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(x => x.ImplicitlyValidateChildProperties = true);

            services
                .AddCustomErrorResponseMessage()
                .AddHttpContextAccessor()
                .AddApplicationLayer(Configuration)
                .AddSecurity(Configuration)
                .AddEntityFrameworkDataAccessImplementation(Configuration)
                .AddSwagger()
                .AddOptions()
                .AddApiServices()
                .AddJwt(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger()
                .UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"))
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}

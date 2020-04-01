using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using QLector.Api.Filters;
using QLector.Application.Extensions;
using QLector.DAL.EF;
using QLector.Security;
using QLector.Security.EFStore;

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
            services.AddControllers(options =>  options.Filters.Add(new ErrorHandlingFilter()))
                .AddNewtonsoftJson(o => o.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services
                .RegisterApplicationLayer()
                .AddJwtTokenBuilder()
                .AddUserService()
                .AddEntityFrameworkIdentity()
                .AddEntityFrameworkDataAccessImplementation(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using IdentityServer.API.Extensions;
using IdentityServer.API.Middlewares;
using Serilog;

namespace IdentityServer.API;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCustomIdentity();
        services.AddCustomControllers();
        services.AddCustomSwagger();
        services.AddCustomAuthentication(configuration);
        services.AddCustomVersioning();
        services.AddCustomLogging();

        services.AddHealthChecks();

        services.AddCustomFluentValidation();

        services.AddCustomCORS();
        services.AddCustomCaching();

        services.AddCustomDomainServices();
        services.AddCustomInfrastructure(configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseMiddleware<ApiKeyMiddleware>();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAllOrigins");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }
}

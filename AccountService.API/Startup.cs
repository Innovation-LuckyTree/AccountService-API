using AccountService.API.Services;
using AccountService.Common.Interfaces;
using Microsoft.OpenApi.Models;
using AccountService.Application;
using AccountService.Infrastructure;
using AccountService.Persistence;

namespace AccountService.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddPolicy(name: "allOrigin",
            policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        string connString = Configuration.GetConnectionString("PaymentDataSource");
        service.AddAuthorization();
        service.AddLogging();
        service.AddControllers();

        service.AddHttpContextAccessor();
        service.AddTransient<ICurrentUserService, CurrentUserService>();

        service.AddConfigurations(Configuration);
        service.AddApplicationLayer();
        service.AddPersistenceLayer(connString);
        service.AddInfrastructureLayer();

        service.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Account Service API", Version = "version 1.0" });
            opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        service.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthentication();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

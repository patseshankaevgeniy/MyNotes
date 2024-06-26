﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Application;
using Persistence;
using Infrastructure;
using TelegramBot;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SignalR.HubConfig;
using WebApi.Infrastructure.Middlewares;
using System.Reflection;
using SignalR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Application dependencies
        services.AddApplicationDependencies(_configuration)
                .AddInfrastructureDependencies(_configuration)
                .AddSignalRDependencies(_configuration)
                .AddTelegramBotDependencies(_configuration)
                .AddPersistenceDependencies(_configuration);

        // Api configuration
        services
            .AddHttpClient()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services
            .AddResponseCompression()
            .AddResponseCaching()
            .AddRouting(opt => opt.LowercaseUrls = true)
            .Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true)
            .AddCors(options => options
                .AddDefaultPolicy(builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials()));

        // Configure Identity
        services
            .AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 4;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Configure SignalR
        services.AddSignalR(cfg => cfg.EnableDetailedErrors = true);

        // Auth configuration
        services
            .AddAuthorization()
            .AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = _configuration["Auth:Issuer"],
                    ValidAudience = _configuration["Auth:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Key"]))
                };
            });

        // Swagger Configurarion
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1.0", new OpenApiInfo
            {
                Title = "Warehouse Api",
                Version = "v1.0"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                 {
                    new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                 }
            });
        });
    }

    public void Configure(IApplicationBuilder builder, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddSerilog();

        builder
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1.0/swagger.json", "Api v1.0");
                options.RoutePrefix = "docs";
            })
            .UseHttpsRedirection()
            .UseMiddleware<CustomExceptionHandlerMiddleware>()
            .UseCors()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseResponseCompression()
            .UseResponseCaching()
            .UseMiddleware<InitializeCurrentUserMiddleware>()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Ok");
                });
                endpoints.MapHub<NotificationHub>("/notifications");
            });
    }
}

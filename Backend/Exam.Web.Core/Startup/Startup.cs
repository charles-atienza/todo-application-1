using Exam.Database.DbContexts;
using Exam.Database.DbContexts.Interface;
using Exam.Database.Repositories.Interface;
using Exam.Extensions;
using Exam.Mappers;
using Exam.Utilities.Configuration;
using Exam.Web.Utilities.Filter;
using Exam.Web.Utilities.Middleware;
using Exam.Web.Utilities.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Extensions;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Integrations;
using System.Reflection;

namespace Exam.Web.Startup;

public class Startup
{
    private const string DefaultCorsPolicyName = "localhost";

    public Startup(IWebHostEnvironment env)
    {
        AppConfiguration = AppplicationConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
    }

    private IConfigurationRoot AppConfiguration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureServices(services, AppConfiguration);
    }

    private void ConfigureServices(IServiceCollection services, IConfigurationRoot appConfiguration)
    {
        var assemblies = AppDomain.CurrentDomain.Load("Exam.BusinessLogic");

        //MVC
        services.AddMvc(options =>
        {
            options.Filters.Add<LoggerContextAttribute>();
            options.Filters.Add<AppExceptionFilterAttribute>();
        });
        ConfigureDbContextService(services);

        services.AddApiVersioning(cfg =>
        {
            cfg.DefaultApiVersion = new ApiVersion(1, 1);
            cfg.AssumeDefaultVersionWhenUnspecified = true;
            cfg.ReportApiVersions = true;
        });

        services.AddSingleton<IConfiguration>(appConfiguration);

        services.AddSignalR();

        services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft);

        services.AddHttpClient();

        ConfigureRepositoryService(services);
        ConfigureMapperlyService(services);

        services.AddMediatR(assemblies);
        services.AddScoped<IMediator, Mediator>();

        //Configure CORS for angular2 UI
        _ = services.AddCors(options =>
        {
            options.AddPolicy(DefaultCorsPolicyName, builder =>
            {
                //App:CorsOrigins in appsettings.json can contain more than one address with split by comma.
                _ = builder
                    .WithOrigins(
                        // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                        appConfiguration.GetValue<string>("App:CorsOrigins")?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostString("/"))
                            .ToArray()!
                    )
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        if (appConfiguration.GetValue<bool>("Swagger:IsUiEnabled"))
        //Swagger - Enable this line and the related lines in Configure method to enable swagger 
        {
            ConfigureSwagger(services);
        }

        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseStatusCodePagesWithRedirects("~/Error?statusCode={0}");
            app.UseExceptionHandler("/Error");
        }

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors(DefaultCorsPolicyName); //Enable CORS!

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });

        if (AppConfiguration.GetValue<bool>("Swagger:IsUiEnabled"))
        {
            app.UseDefaultFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(AppConfiguration["Swagger:EndPoint"], "Exam API V1");
            }); //URL: /swagger
        }

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }

    private void ConfigureDbContextService(IServiceCollection services)
    {
        services.AddDbContext<ExamDbContext>(options =>
        {
            options.UseSqlServer(
                AppConfiguration.GetConnectionString(ExamConstants.ConnectionStringName),
                x => x.MigrationsAssembly("Exam.EntityFrameworkCore")
            );

            if (AppConfiguration.GetValue<bool>("App:DbEnableSensitiveDataLogging"))
            {
                options.EnableSensitiveDataLogging();
            }
        });

        services.AddScoped<IDbContextProvider<ExamDbContext>, DbContextProvider<ExamDbContext>>();
    }

    /// <summary>
    ///     Configure services for Mapperly
    /// </summary>
    /// <param name="services"></param>
    private void ConfigureMapperlyService(IServiceCollection services)
    {
        var repositoryAssembly = AppDomain.CurrentDomain.Load("Exam.BusinessLogic");
        var mappersList =
            repositoryAssembly.GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces()
                    .Any(i => i == typeof(IMapper) ||
                              (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper)))
                );

        foreach (var mappers in mappersList)
        {
            var interfaceType = mappers.GetInterfaces().FirstOrDefault(i => i.Name == $"I{mappers.Name}");
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, mappers);
            }
        }
    }

    private void ConfigureRepositoryService(IServiceCollection services)
    {
        var repositoryAssembly = AppDomain.CurrentDomain.Load("Exam.Database");
        var repositoryTypes = repositoryAssembly
            .GetTypes()
            .Where(t => t.IsClass && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>)));
        foreach (var repositoryType in repositoryTypes)
        {
            var interfaceType = repositoryType.GetInterfaces().FirstOrDefault(i => i.Name == $"I{repositoryType.Name}");
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, repositoryType);
            }
        }
    }

    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Exam API", Version = "v1" });
            options.DocInclusionPredicate((_, _) => true);
            options.ParameterFilter<SwaggerEnumParameterFilter>();
            options.SchemaFilter<SwaggerEnumSchemaFilter>();
            options.OperationFilter<SwaggerOperationIdFilter>();
            options.OperationFilter<SwaggerOperationFilter>();

            //add summaries to swagger
            var canShowSummaries = AppConfiguration.GetValue<bool>("Swagger:ShowSummaries");

            if (!canShowSummaries)
            {
                return;
            }

            var hostXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var hostXmlPath = Path.Combine(AppContext.BaseDirectory, hostXmlFile);
            options.IncludeXmlComments(hostXmlPath);

            const string applicationXml = "Exam.Application.xml";
            var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXml);
            options.IncludeXmlComments(applicationXmlPath);

            const string webCoreXmlFile = "Exam.Web.Core.xml";
            var webCoreXmlPath = Path.Combine(AppContext.BaseDirectory, webCoreXmlFile);
            options.IncludeXmlComments(webCoreXmlPath);
        });
    }
}
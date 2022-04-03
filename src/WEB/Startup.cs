using Microsoft.AspNetCore.Identity;
using WEB.Infrastructure.Initializers;
using Infrastructure.DataAccess;
using WEB.Infrastructure.Startup;
using Domain;
using Infrastructure.Abstractions;
using Infrastructure;
using WEB.Infrastructure.MappingProfiles;
using MediatR;
using UseCases;
using Infrastructure.Common;

namespace Saritasa.People.Web;

/// <summary>
/// Entry point for ASP.NET Core app.
/// </summary>
public class Startup
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Entry point for web application.
    /// </summary>
    /// <param name="configuration">Global configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Configure application services on startup.
    /// </summary>
    /// <param name="services">Services to configure.</param>
    /// <param name="environment">Application environment.</param>
    public void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment)
    {
        // Add controllers.
        services.AddControllers();

        // Database.
        services.AddDbContext<AppDbContext>(
            new DbContextOptionsSetup(configuration.GetConnectionString("AppDatabase")).Setup);
        services.AddAsyncInitializer<DatabaseInitializer>();
        services.AddScoped<IAppDbContext, AppDbContext>();

        // Swagger.
        services.AddSwaggerGen();

        // Identity.
        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(new IdentityOptionsSetup().Setup);

        // Add authentication.
        services.AddAuthentication();

        // Add authorization
        services.AddAuthorization();

        // Automapper.
        services.AddAutoMapper(typeof(FileMappingProfile).Assembly);

        // Other dependencies.
        services.AddMediatR(typeof(DeleteFileCommand).Assembly);
        services.AddTransient<IFileStorage, LocalFileStorage>();
        services.AddTransient<ILoggedUserAccessor, LoggedUserAccessor>();
    }

    /// <summary>
    /// Configure web application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="environment">Application environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapSwagger();
            endpoints.MapControllers();
        });
    }
}

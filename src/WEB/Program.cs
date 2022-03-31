namespace Saritasa.People.Web;

/// <summary>
/// Entry point class.
/// </summary>
internal sealed class Program
{
    private static WebApplication app;

    /// <summary>
    /// Entry point method.
    /// </summary>
    /// <param name="args">Program arguments.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services, builder.Environment);
        app = builder.Build();
        startup.Configure(app, app.Environment);
        await app.InitAsync();
        await app.RunAsync();
    }
}

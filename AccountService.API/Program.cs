namespace AccountService.API;
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    // public static IHostBuilder CreateHostBuilder(string[] args)
    // {
    //     return Host.CreateDefaultBuilder(args)
    //         .ConfigureAppConfiguration((hostingContext, config) =>
    //         {
    //             config.AddEnvironmentVariables()
    //                 .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
    //                 .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    //                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    //             //.EnableSubstitutions("%", "%", exceptOnMissingVariables: false);
    //         })
    //         .ConfigureWebHostDefaults(webBuilder =>
    //         {
    //             webBuilder.UseStartup<Startup>();
    //         });
    // }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
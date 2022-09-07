using FluentMigrator.Runner;
namespace Database;

public class Program
{
    public static void Main(string[] args)
    {
        //var builder = WebApplication.CreateBuilder(args);
        //var app = builder.Build();
        //app.MapGet("/", () => "Hello World!");
        //app.Run();

        var serviceProvider = CreateServices();

        using (var scope = serviceProvider.CreateScope())
        {
            UpdateDatabase(scope.ServiceProvider);
        }
    }

    private static IServiceProvider CreateServices()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();

        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                // Set the connection string
                .WithGlobalConnectionString(configuration["connectionString"])
                // Define the assembly containing the migrations
                .ScanIn(typeof(Migrations._001_AddCategoryTable).Assembly).For.Migrations())

            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            // Build the service provider
            .BuildServiceProvider(false);
    }

    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.MigrateUp();
    }
}

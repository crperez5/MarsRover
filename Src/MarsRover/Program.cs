using MarsRover.Application;
using MarsRover.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<MarsRoverApp>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            var config = LoadConfiguration();

            var logDirectory = config.GetValue<string>("Runtime:LogOutputDirectory");
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(logDirectory)
                .WriteTo.Console()
                .CreateLogger();

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(config);

            services.AddSingleton<MarsRoverApp>();
            services.AddSingleton(_ =>
            {
                var minCoordinates = int.Parse(config["MarsRover:MinCoordinates"]);
                var maxCoordinates = int.Parse(config["MarsRover:MaxCoordinates"]);
                var rover = MarsRoverDeployer.Deploy(minCoordinates, maxCoordinates);
                Log.Information("Rover touched down on Mars! Current status: {@rover}", rover);
                return rover;
            });
            services.AddSingleton<ICommandParser, CommandParser>();

            services.AddSingleton(provider =>
            {
                var rover = provider.GetRequiredService<IMarsRover>();

                return new CommandPipeline()
                    .Pipe(new MoveForwardCommandHandler(rover, provider.GetRequiredService<ILogger<MoveForwardCommandHandler>>()))
                    .Pipe(new UnknownCommandHandler(provider.GetRequiredService<ILogger<UnknownCommandHandler>>()));
            });

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}

using CS_lab.FoodGenerator;
using CS_lab.GameStateWriter;
using CS_lab.NameGenerator;
using CS_lab.WormStrategy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CS_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<SimulatorService>();
                    services.AddScoped<IFoodGenerator, NormallyDistributedFoodGenerator>();
                    services.AddScoped<INameGenerator, RandomNameGenerator>();
                    services.AddScoped<IWormStrategy, NearestFoodStrategy>();
                    services.AddScoped<IGameStateWriter>(_ => new GameStateFileWriter("game_logs.txt"));
                });
        }
    }
}
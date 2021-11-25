using System;
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
                    
                    services.AddScoped<INameGenerator, RandomNameGenerator>();
                    services.AddScoped<IWormStrategy>(_ => new RemoteWormStrategy("192.168.99.100", "5000"));
                    //services.AddScoped<IWormStrategy>(_ => new NearestFoodStrategy());
                    services.AddScoped<IGameStateWriter>(_ => new GameStateFileWriter("game_logs.txt"));

                    if (args.Length > 0)
                    {
                        services.AddScoped<IFoodGenerator>(_ => new BehaviorFoodGenerator(args[0]));
                    }
                    else
                    {
                        services.AddScoped<IFoodGenerator, NormallyDistributedFoodGenerator>();
                    }
                });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualBasic.CompilerServices;
using WorldBehavior.BehaviorWriter;
using WorldBehavior.FoodGenerator;
using WorldBehavior.Models;
using WorldBehavior.Utils;

namespace WorldBehavior
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var randomNameGenerator = new RandomNameGenerator(8);
            var behaviorName = randomNameGenerator.NextName();

            if (args.Length > 0)
            {
                behaviorName = args[0];
            }

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService(serviceProvider => new BehaviorService(
                        serviceProvider.GetService<IHostApplicationLifetime>(),
                        serviceProvider.GetService<ApplicationContext>(),
                        serviceProvider.GetService<IFoodGenerator>(),
                        serviceProvider.GetService<IBehaviorWriter>(),
                        behaviorName
                    ));
                    services.AddDbContext<ApplicationContext>();
                    services.AddScoped<IFoodGenerator, NormallyDistributedFoodGenerator>();
                    services.AddScoped<IBehaviorWriter, BehaviorWriter.BehaviorWriter>();
                });
        }
    }
}
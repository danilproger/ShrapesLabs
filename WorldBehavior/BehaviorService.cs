using WorldBehavior.FoodGenerator;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WorldBehavior.BehaviorWriter;

namespace WorldBehavior
{
    public class BehaviorService : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ApplicationContext _db;
        private readonly IFoodGenerator _foodGenerator;
        private readonly IBehaviorWriter _writer;
        private readonly string _behaviorName;

        public BehaviorService(
            IHostApplicationLifetime appLifetime, 
            ApplicationContext context, 
            IFoodGenerator generator, 
            IBehaviorWriter writer,
            string behaviorName)
        {
            _appLifetime = appLifetime;
            _db = context;
            _foodGenerator = generator;
            _writer = writer;
            _behaviorName = behaviorName;
        }

        public void Write()
        {
            _writer.Write(_behaviorName, _db, _foodGenerator);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                Write();
                _appLifetime.StopApplication();
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
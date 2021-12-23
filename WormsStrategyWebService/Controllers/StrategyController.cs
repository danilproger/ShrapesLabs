using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WormsStrategyWebService.Models;
using WormsStrategyWebService.WormStrategy;

namespace WormsStrategyWebService.Controllers
{
    [ApiController]
    [Route("worms")]
    public class StrategyController: ControllerBase
    {
        private readonly IWormStrategy _wormStrategy;

        public StrategyController(IWormStrategy strategy)
        {
            _wormStrategy = strategy;
        }
        
        [HttpPost("{name}/getAction")]
        public WormGameStep GetNextStep(string name, [FromBody] World world, int step, int run)
        {
            var currentWorm = world.Worms.FirstOrDefault(worm => worm.Name.Equals(name));
            
            if (currentWorm == null)
            {
                return new WormGameStep();
            }
            
            return _wormStrategy.NextStep(currentWorm, world, step);
        }
        
    }
}
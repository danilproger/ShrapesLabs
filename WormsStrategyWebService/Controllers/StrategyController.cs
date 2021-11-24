using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WormsStrategyWebService.Models;
using WormsStrategyWebService.WormStrategy;

namespace WormsStrategyWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StrategyController: ControllerBase
    {
        private readonly IWormStrategy _wormStrategy;

        public StrategyController(IWormStrategy strategy)
        {
            _wormStrategy = strategy;
        }
        
        [HttpPost("{name}/getAction")]
        public WormGameStep GetNextStep(string name, [FromBody] WorldGameState gameState)
        {
            var currentWorm = gameState.World.Worms.FirstOrDefault(worm => worm.Name.Equals(name));
            
            if (currentWorm == null)
            {
                return new WormGameStep(WormStep.Nothing, Direction.Nothing);
            }
            
            return _wormStrategy.NextStep(currentWorm, gameState.World, gameState.GameStep);
        }
        
    }
}
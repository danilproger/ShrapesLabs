namespace WormsStrategyWebService.Models
{
    public class WorldGameState
    {
        public World World { get; set; }
        public int GameStep { get; set; }

        public WorldGameState(World world, int gameStep)
        {
            World = world;
            GameStep = gameStep;
        }
    }
}
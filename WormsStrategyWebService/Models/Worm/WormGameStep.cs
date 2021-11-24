namespace WormsStrategyWebService.Models
{
    public class WormGameStep
    {
        public WormStep WormStep { get; set; }
        public Direction Direction { get; set; }
        
        public WormGameStep()
        {
        }

        public WormGameStep(WormStep wormStep, Direction direction)
        {
            WormStep = wormStep;
            Direction = direction;
        }
    }
}
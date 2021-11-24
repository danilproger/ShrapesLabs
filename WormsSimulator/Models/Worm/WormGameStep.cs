using System.Text.Json.Serialization;

namespace CS_lab.Models
{
    public class WormGameStep
    {
        [JsonPropertyName("wormStep")]
        public WormStep WormStep { get; set; }
        [JsonPropertyName("direction")]
        public Direction Direction { get; set; }
        
        /*public WormGameStep()
        {
        }*/

        public WormGameStep(WormStep wormStep, Direction direction)
        {
            WormStep = wormStep;
            Direction = direction;
        }
    }
}
using System.Text.Json.Serialization;

namespace WormsStrategyWebService.Models
{
    public class WormGameStep
    {
        [JsonPropertyName("direction")]
        public string Direction { get; set; }
        [JsonPropertyName("split")]
        public bool Split { get; set; }
        
        public WormGameStep()
        {
            Direction = "Up";
            Split = false;
        }

        public WormGameStep(Direction direction, bool split)
        {
            Direction = direction.ToString();
            Split = split;
        }
    }
}
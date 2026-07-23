using System.Text.Json.Serialization;

namespace SimpleCarDb.Models
{
    public class EngineDetail
    {
        public int Id { get; set; }
        public string EngineNumber { get; set; } = string.Empty;
        public int CapacityCc { get; set; }
        public int Horsepower { get; set; }
        public int? CarId { get; set; } //nullable lett a drágám

        [JsonIgnore]
        public Car? Car { get; set; }
    }
}

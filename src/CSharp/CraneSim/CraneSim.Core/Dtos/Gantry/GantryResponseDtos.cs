using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Dtos.Gantry
{
    public class GantryResponseDtos
    {
        public int Id { get; set; }

        [JsonPropertyName("component")]
        public string Name { get; set; }

        [JsonPropertyName("relativePosition")]
        public float PositionZ { get; set; } = 0.0F;

        [JsonPropertyName("speed")]
        public float Speed { get; set; } = 0.0F;

        [JsonPropertyName("acceleration")]
        public float Acceleration { get; set; } = 0.0F;

        [JsonPropertyName("active")]        
        public bool IsActive { get; set; } = false;
    }
}

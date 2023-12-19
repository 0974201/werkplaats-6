using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Hoist
{
    public class HoistResponseSpeedDto
    {
        [JsonPropertyName("activeAcceleration")]
        public bool ActiveAcceleration { get; set; }
        [JsonPropertyName("acceleration")]
        public float Acceleration { get; set; }
        [JsonPropertyName("speed")]
        public float Speed { get; set; }
    }
}

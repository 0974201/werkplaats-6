using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Trolley
{
    public class TrolleyResponseSpeedDto
    {
        [JsonPropertyName("activeAcceleration")]
        public bool ActiveAcceleration { get; set; }

        [JsonPropertyName("acceleration")]
        public float Acceleration { get; set; }

        [JsonPropertyName("speed")]
        public float Speed { get; set; }
    }
}

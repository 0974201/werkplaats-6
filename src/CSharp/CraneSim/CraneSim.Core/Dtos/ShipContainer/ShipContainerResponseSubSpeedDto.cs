using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerResponseSubSpeedDto
    {
        [JsonPropertyName("x")]
        public float PositionX { get; set; }
        [JsonPropertyName("y")]
        public float PositionY { get; set; }
        [JsonPropertyName("z")]
        public float PositionZ { get; set; }
    }
}

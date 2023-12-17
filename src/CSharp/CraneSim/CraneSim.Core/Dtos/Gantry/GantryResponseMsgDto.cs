using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Gantry
{
    public class GantryResponseMsgDto
    {
        [JsonPropertyName("relativePosition")]
        public float PositionZ { get; set; } = 0.0F;

        [JsonPropertyName("speed")]
        public GantryResponseSpeedDto Speed { get; set; }
    }
}

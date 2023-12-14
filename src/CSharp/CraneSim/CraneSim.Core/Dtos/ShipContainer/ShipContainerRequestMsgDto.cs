using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerRequestMsgDto
    {
        [JsonPropertyName("isConnected")]
        public bool IsConnected { get; set; }

        [JsonPropertyName("relativePosition")]
        public ShipContainerRequestRelPosDto RelativePosition { get; set; }

        [JsonPropertyName("speed")]
        public ShipContainerRequestSpeedDto Speed { get; set; }
    }
}

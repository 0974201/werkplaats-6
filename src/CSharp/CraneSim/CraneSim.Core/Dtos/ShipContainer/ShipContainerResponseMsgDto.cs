using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerResponseMsgDto
    {
        [JsonPropertyName("isConnected")]
        public bool IsConnected { get; set; }

        [JsonPropertyName("absolutePosition")]
        public ShipContainerResponseAbsolPosDto ShipCont { get; set; }

        [JsonPropertyName("speed")]
        public ShipContainerResponseSpeedDto Speed { get; set; }
    }
}

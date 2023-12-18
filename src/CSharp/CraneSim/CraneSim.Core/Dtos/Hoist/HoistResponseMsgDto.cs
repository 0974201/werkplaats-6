using CraneSim.Core.Dtos.ShipContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Hoist
{
    public class HoistResponseMsgDto
    {
        [JsonPropertyName("isConnected")]
        public bool IsConnected { get; set; }

        [JsonPropertyName("relativePosition")]
        public HoistResponseRelPosDto RelativePosition { get; set; }

        [JsonPropertyName("speed")]
        public HoistResponseSpeedDto Speed { get; set; }
    }
}

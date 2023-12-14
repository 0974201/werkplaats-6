using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerResponseDto
    {
        [JsonPropertyName("meta")]
        public ShipContainerResponseMetaDto Meta { get; set; }

        [JsonPropertyName("msg")]
        public ShipContainerResponseSpeedDto Msg { get; set; }
    }
}

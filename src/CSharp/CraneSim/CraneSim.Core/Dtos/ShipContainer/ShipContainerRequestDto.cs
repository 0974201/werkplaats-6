using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerRequestDto
    {
        [JsonPropertyName("meta")]
        public ShipContainerRequestMetaDto Meta { get; set; }

        [JsonPropertyName("msg")]
        public ShipContainerRequestMsgDto Msg { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CraneSim.Core.Dtos.Gantry
{
    public class GantryResponseDto
    {
        [JsonPropertyName("meta")]
        public GantryResponseMetaDto Meta { get; set; }

        [JsonPropertyName("msg")]
        public GantryResponseMsgDto Msg { get; set; }
    }
}

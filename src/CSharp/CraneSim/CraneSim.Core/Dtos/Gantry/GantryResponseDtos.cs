using CraneSim.Core.Dtos.Trolley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Dtos.Gantry
{
    public class GantryResponseDtos
    {
        [JsonPropertyName("meta")]
        public TrolleyResponseMetaDto Meta { get; set; }

        [JsonPropertyName("msg")]
        public TrolleyResponseMsgDto Msg { get; set; }
    }
}

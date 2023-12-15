using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Trolley
{
    public class TrolleyResponseDto
    {
        [JsonPropertyName("meta")]
        public TrolleyResponseMetaDto Meta { get; set; }

        [JsonPropertyName("msg")]
        public TrolleyResponseMsgDto Msg { get; set; }
    }
}

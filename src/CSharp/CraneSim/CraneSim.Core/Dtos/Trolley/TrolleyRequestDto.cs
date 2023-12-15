using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Trolley
{
    public class TrolleyRequestDto
    {
        [JsonPropertyName("meta")]
        public TrolleyRequestMetaDto Meta { get; set; }
        [JsonPropertyName("msg")]
        public TrolleyRequestMsgDto Msg { get; set; }
    }
}

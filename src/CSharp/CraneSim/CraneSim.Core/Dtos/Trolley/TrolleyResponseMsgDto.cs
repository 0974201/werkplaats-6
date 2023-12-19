using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Trolley
{
    public class TrolleyResponseMsgDto
    {
        
        [JsonPropertyName("relativePosition")]
        public float RelativePosition { get; set; }

        [JsonPropertyName("speed")]
        public TrolleyResponseSpeedDto Speed { get; set; }
    }
}

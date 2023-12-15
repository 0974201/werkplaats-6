using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Trolley
{
    public class TrolleyResponseMetaDto
    {
        [JsonPropertyName("topic")]
        public string Topic { get; set; } = "crane/components/trolley/state";
        [JsonPropertyName("active")]
        public bool IsActive { get; set; } = false;
        [JsonPropertyName("component")]
        public string Component { get; set; } = "trolley";

    }
}

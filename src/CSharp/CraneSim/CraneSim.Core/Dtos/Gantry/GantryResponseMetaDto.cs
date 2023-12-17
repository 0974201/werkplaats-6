using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Gantry
{
    public class GantryResponseMetaDto
    {
        [JsonPropertyName("topic")]
        public string Topic { get; set; } = "gantry/state";

        [JsonPropertyName("active")]
        public bool IsActive { get; set; } = false;

        [JsonPropertyName("component")]
        public string Name { get; set; } = "gantry";

    }
}

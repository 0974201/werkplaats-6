using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerRequestMetaDto
    {
        [JsonPropertyName("topic")]
        public string Topic { get; set; }
        [JsonPropertyName("isActive")]
        public bool IActive { get; set; }
        [JsonPropertyName("component")]
        public string Component { get; set; }
    }
}

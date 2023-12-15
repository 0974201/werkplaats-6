using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerResponseMetaDto
    {
        [JsonPropertyName("topic")]
        public string Topic { get; set; } = "containers/id/state";
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("component")]
        public string Component { get; set; } = "container";

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Meta
{
    public class MetaRequestDto
    {
        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("component")]
        public string Component { get; set; }
    }
}

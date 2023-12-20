using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CraneSim.Core.Dtos.Gantry
{
    public class GantryRequestMetaDto
    {
        [JsonPropertyName("topic")]
        public string Topic { get; set; }
    }
}

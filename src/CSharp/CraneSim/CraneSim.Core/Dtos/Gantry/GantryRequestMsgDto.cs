using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CraneSim.Core.Dtos.Gantry
{
    public class GantryRequestMsgDto
    {
        [JsonPropertyName("target")]
        public string Target { get; set; }

        [JsonPropertyName("command")]
        public string Command { get; set; }
    }
}

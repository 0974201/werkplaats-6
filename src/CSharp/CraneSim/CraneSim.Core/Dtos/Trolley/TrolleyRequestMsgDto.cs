using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Trolley
{
    public class TrolleyRequestMsgDto
    {
        [JsonPropertyName("target")]
        public string Target { get; set; }

        [JsonPropertyName("command")]
        public string Command { get; set; }
    }
}

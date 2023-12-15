using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Hoist
{
    public class HoistRequestMsgDto
    {
        [JsonPropertyName("relativePosition")]
        public HoistRequestRelPosDto RelativePosition { get; set; }
    }
}

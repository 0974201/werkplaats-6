using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Hoist
{
    public class HoistResponseMsgDto
    {
        [JsonPropertyName("relativePosition")]
        public HoistResponseRelPosDto RelativePosition { get; set; }
    }
}

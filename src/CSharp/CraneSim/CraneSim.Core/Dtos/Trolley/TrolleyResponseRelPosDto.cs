using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Trolley
{
    public class TrolleyResponseRelPosDto
    {
        [JsonPropertyName("x")]
        public float X { get; set; }
    }
}

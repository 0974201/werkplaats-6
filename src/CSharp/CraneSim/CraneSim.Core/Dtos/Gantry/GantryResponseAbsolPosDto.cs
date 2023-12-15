using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Crane
{
    public class GantryResponseAbsolPosDto
    {
        [JsonPropertyName("z")]
        public float PositionZ { get; set; }
    }
}

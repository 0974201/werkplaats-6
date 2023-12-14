using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerRequestRelPosDto
    {
        [JsonPropertyName("y")]
        public float PositionY { get; set; }
    }
}

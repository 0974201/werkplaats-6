using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.ShipContainer
{
    public class ShipContainerResponseSpeedDto
    {
        [JsonPropertyName("speed")]
        public ShipContainerResponseSubSpeedDto Speed { get; set; }
    }
}

using CraneSim.Core.Dtos.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Main
{
    public class MainResponseDto
    {
        [JsonPropertyName("meta")]
        public MetaResponseDto Meta { get; set; }
    }
}

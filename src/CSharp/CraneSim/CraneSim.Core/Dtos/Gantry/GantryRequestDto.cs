using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CraneSim.Core.Dtos.Gantry;

namespace CraneSim.Dtos.Gantry
{
    public class GantryRequestDto
    { //https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5

        [JsonPropertyName("meta")]
        public GantryRequestMetaDto Meta { get; set; }
        [JsonPropertyName("msg")]
        public GantryRequestMsgDto Msg { get; set; }
    }
}

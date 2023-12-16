using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Dtos.Gantry
{
    public class GantryRequestDto
    { //https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5

        [JsonPropertyName("topic")]
        public string Topic { get; set; }
    }
}

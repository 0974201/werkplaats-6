﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraneSim.Core.Dtos.Gantry
{
    public class GantryResponseSpeedDto
    {
        [JsonPropertyName("acceleration")]
        public float Acceleration { get; set; }

        [JsonPropertyName("speed")]
        public float Speed { get; set; }
    }
}

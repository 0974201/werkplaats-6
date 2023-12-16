using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Dtos.Gantry
{
    public class GantryRequestDto
    { //https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
        public int Id { get; set; }
        public string Name { get; set; }

        public float PositionZ { get; set; } = 0.0F;
        public float Speed { get; set; } = 0.0F;
        public float Acceleration { get; set; } = 0.0F;

        public bool IsActive { get; set; } = false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Core.Entities
{
    public class Gantry
    {
        public string Component { get; set; } = "Gantry"; //default value

        public float PositionZ { get; set; } = 0.0F; //ik denk dat ik double moet gebruiken hier ipv float?

        public float Speed { get; set; } = 0.0F;
        public float Acceleration { get; set; } = 0.0F;

        public bool IsActive { get; set; } = false;
    }
}

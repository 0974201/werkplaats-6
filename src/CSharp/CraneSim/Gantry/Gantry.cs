using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantry
{
    public class Gantry
    {

        public string Component { get; set }

        public float PositionZ { get; set }

        public float Speed { get; set; }
        public float Acceleration { get; set; }

        public bool IsActive {  get; set; }


        public Gantry()
        {

        }
    }
}
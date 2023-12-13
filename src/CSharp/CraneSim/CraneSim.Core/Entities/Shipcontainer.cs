using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Core.Entities
{
    public class Shipcontainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsConnectedToHoist { get; set; } = false;
        public double Height { get; set; } = 2.59;
        public double Width { get; set; } = 2.44;
        public double Length { get; set; } = 13.71; 
        //afmetingen zijn volgens het voorbeeld in de opgave
        public float PositionX { get; set; } = 0.0F;
        public float PositionY { get; set; } = 0.0F;
        public float PositionZ { get; set; } = 0.0F;
        //startposities zijn 0
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trolley.Entities
{
    public class Trolley
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsOn { get; set; } = false;

        public double Speed { get; set; } = 0.0;
        public double Acceleration { get; set; } = 0.0;
        public double Deceleration { get; set; } = 0.0;
        public double TraveledDistance { get; set; } = 0.0;

        public double Height { get; set; } = 1.0;
        public double Width { get; set; } = 9.0;
        public double Length { get; set; } = 2.50;

        public int Mass { get; set; }

        public double PositionX { get; set; } = 0.0;
        public double PositionY { get; set; } = 0.0;
        public double PrositionZ { get; set; } = 0.0;


        public Trolley()
        {

        }
    }
}

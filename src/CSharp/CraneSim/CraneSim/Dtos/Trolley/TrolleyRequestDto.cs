using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Dtos.Trolley
{
    public class TrolleyRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = false;
        public float Speed { get; set; } = 0.0F;
        public float Acceleration { get; set; } = 0.0F;
        public float PositionX { get; set; } = 0.0F;
    }
}

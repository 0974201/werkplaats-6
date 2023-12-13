using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Core.Entities
{
    public class Gantry
    {
        public int Id { get; set; }
        public string Name { get; set; } //default value

        //public float StartingPosition { get; set; } = 1000.0F;
        public float PositionZ { get; set; } = 0.0F; //ik denk dat ik double moet gebruiken hier ipv float?
        public float MinPosZ { get; set; } = 0.0F;
        public float MaxPosZ { get; set; } = 1000.0F;

        public float Speed { get; set; } = 0.0F;
        public float Acceleration { get; set; } = 0.0F;
        public float Deceleration { get; set; } = 0.0F;
        public float TravelledDistance { get; set; } = 0.0F;
        public float AccelAndDecelTime { get; set; } = 1.5F;

        public double Height { get; set; } = 0.0F;
        public double Width { get; set; } = 0.0F;
        public double Length { get; set; } = 0.0F;

        public float MinimumSpeedValue { get; set; } = 2.0F;
        public float MaximumSpeedValue { get; set; } = 3.0F;
        public float MinimumAccelerationValue { get; set; } = 0.5F;
        public float MaximumAccelerationValue { get; set; } = 0.5F;

        public bool IsActive { get; set; } = false;
    }
}

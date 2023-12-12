using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Core.Entities
{
    public class Trolley
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; } = false;

        public float Speed { get; set; } = 0.0F;
        
        // wordt in m/s² weergegeven
        public float Acceleration { get; set; } = 0.0F;
        public float Deceleration { get; set; } = 0.0F;
        public float TraveledDistance { get; set; } = 0.0F;

        public float AccelAndDecelarationTime { get; set; } = 2F;

        //in m/sec
        public float MinimumSpeedValue { get; set; } = 2.5F;
        //in m/sec
        public float MaximumSpeedValue { get; set; } = 3.0F;
        //in m/sec
        public float MinimumAccelerationValue { get; set; } = 0.5F;
        //in m/sec
        public float MaximumAccelerationValue { get; set; } = 0.8F;


        public double Height { get; set; } = 1.0;
        public double Width { get; set; } = 9.0;
        public double Length { get; set; } = 2.50;


        public float PositionX { get; set; } = 0.0F;
        public float MinPositionX { get; set; } = 0.0F;
        public float MaxPositionX { get; set; } = 111.0F;
    }
}

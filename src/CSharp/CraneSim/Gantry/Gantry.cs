using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantry
{
    public class Gantry
    {

        public string Component { get; set; } = "Gantry"; //default value

        public float PositionZ { get; set; } = 0; //ik denk dat ik double moet gebruiken hier ipv float?

        public float Speed { get; set; } = 0;
        public float Acceleration { get; set; } = 0;

        public bool IsActive { get; set; } = false;


        public Gantry()
        {

        }

        /*static void Main(string[] args)
        {
            Gantry gantry = new Gantry();
            
            Console.WriteLine(gantry.Component); //does this work?
            Console.WriteLine(gantry.PositionZ);
            Console.WriteLine(gantry.Speed);
            Console.WriteLine(gantry.Acceleration);
            Console.WriteLine(gantry.IsActive);
        }*/
    }
}
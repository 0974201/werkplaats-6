using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;

namespace CraneSim.Core.Services
{
    public class GantryService : IGantryService
    {
        public readonly Stopwatch _gantryMoveStopwatch;

        public GantryService() 
        {
            _gantryMoveStopwatch = new Stopwatch();
        }
        public float CalculateAccelaration(Gantry entity)
        {
            throw new NotImplementedException();
        }

        public float CalculateCurrentSpeed(Gantry entity)
        {
            throw new NotImplementedException();
        }

        public float CalculateNegativeMovement(Gantry entity)
        {
            throw new NotImplementedException();
        }

        public float CalculatePositiveMovement(Gantry entity)
        {
            throw new NotImplementedException();
        }

        public void ResetStopWatch()
        {
            _gantryMoveStopwatch.Reset();
        }

        public double ReturnStopwatchvalue()
        {
            return (double)(_gantryMoveStopwatch.ElapsedMilliseconds) / 1000;
        }

        public void StartStopwatch()
        {
            _gantryMoveStopwatch.Start();
        }

        public void StopStopwatch()
        {
            _gantryMoveStopwatch.Stop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CraneSim.Core.Entities;

namespace CraneSim.Core.Interfaces
{
    public interface IGantryService
    {
        float CalculatePositiveMovement(Gantry entity);
        float CalculateNegativeMovement(Gantry entity);
        float CalculateCurrentSpeed(Gantry entity);
        float CalculateAccelaration(Gantry entity);
        void ResetStopWatch();
        void StartStopwatch();
        void StopStopwatch();
        double ReturnStopwatchvalue();
    }
}

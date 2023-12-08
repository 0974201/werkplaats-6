using CraneSim.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Core.Interfaces
{
    public interface ITrolleyService
    {
        float CalculateHorizontalPositiveMovement(Trolley entity);
        float CalculateHorizontalNegativeMovement(Trolley entity);
        float CalculateCurrentSpeed(Trolley entity);
        float CalculateConstantAccelaration(Trolley entity);
        void ResetStopWatch();
        void StartStopwatch();
        void StopStopwatch();
        double ReturnStopwatchvalue();
    }
}

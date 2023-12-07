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
        Task<float> CalculateHorizontalePositiefMovement(Trolley entity);
        Task<float> CalculateHorizontaleNegatiefMovement(Trolley entity);
        Task<float> CalculateCurrentSpeed(Trolley entity);
        Task<float> CalculateConstantAccelaration(Trolley entity);
        Task ResetStopWatch();
        Task StartStopwatch();
        Task StopStopwatch();
    }
}

using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Core.Services
{
    public class TrolleyService : ITrolleyService
    {
        public readonly Stopwatch _trolleyMoveStopwatch;

        public TrolleyService()
        {
            _trolleyMoveStopwatch = new Stopwatch();
        }

        public async Task<float> CalculateConstantAccelaration(Trolley entity)
        {
            var accelerationTime = entity.AccelAndDecelarationTime;
            var currentSpeed = entity.Speed;
            var topspeed = entity.MaximumSpeedValue;

            float result = (topspeed - currentSpeed) / accelerationTime;

            entity.Acceleration = result;
            return result;
        }

        public async Task<float> CalculateCurrentSpeed(Trolley entity)
        {
            var timePast = (float)(_trolleyMoveStopwatch.ElapsedMilliseconds / 1000);

            if (timePast < entity.AccelAndDecelarationTime)
            {
                entity.Speed = entity.Acceleration * timePast;
            }
            else if (timePast < 2 * entity.AccelAndDecelarationTime)
            {
                entity.Speed = entity.MaximumSpeedValue - entity.Deceleration * (timePast - entity.AccelAndDecelarationTime);
            }
            else
            {
                entity.Speed = 0;
            }

            entity.Speed = Math.Max(entity.MaximumSpeedValue, Math.Min(entity.MinimumSpeedValue, entity.Speed));

            return entity.Speed;
        }

        public async Task<float> CalculateHorizontaleNegatiefMovement(Trolley entity)
        {

            var timePast = (float)(_trolleyMoveStopwatch.ElapsedMilliseconds / 1000.0);
            var currentSpeed = entity.Speed;
            var travelledDistance = currentSpeed * timePast;

            float newPositionX = entity.PositionX - travelledDistance;

            return newPositionX;
        }

        public async Task<float> CalculateHorizontalePositiefMovement(Trolley entity)
        {
            var timePast = (float)(_trolleyMoveStopwatch.ElapsedMilliseconds / 1000.0);
            var currentSpeed = entity.Speed;
            var travelledDistance = currentSpeed * timePast;

            float newPositionX = entity.PositionX + travelledDistance;

            return newPositionX;
        }

        public async Task ResetStopWatch()
        {
            _trolleyMoveStopwatch.Reset();
        }

        public async Task StartStopwatch()
        {
            _trolleyMoveStopwatch.Start();
        }

        public async Task StopStopwatch()
        {
            _trolleyMoveStopwatch.Stop();
        }

        public async Task<long> ReturnStopwatchvalue()
        {
            return _trolleyMoveStopwatch.ElapsedMilliseconds / 1000;
        }
    }
}

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

        public float CalculateConstantAccelaration(Trolley entity)
        {
            var accelerationTime = entity.AccelAndDecelarationTime;
            var currentSpeed = entity.Speed;
            var topspeed = entity.MaximumSpeedValue;

            float result = (topspeed - currentSpeed) / accelerationTime;

            entity.Acceleration = result;
            return result;
        }

        public float CalculateCurrentSpeed(Trolley entity)
        {
            var timePast = (float)ReturnStopwatchvalue();

            if (timePast < entity.AccelAndDecelarationTime)
            {
                entity.Speed = entity.Acceleration * timePast;
            }
            else
            {
                entity.Speed = entity.MaximumSpeedValue;
            }

            entity.Speed = Math.Min(entity.MaximumSpeedValue, entity.Speed);

            return entity.Speed;
        }

        public float CalculateHorizontalNegativeMovement(Trolley entity)
        {

            var timePast = (float)ReturnStopwatchvalue();
            var currentSpeed = entity.Speed;
            var travelledDistance = currentSpeed * timePast;

            float newPositionX = entity.PositionX - travelledDistance;

            if (newPositionX < entity.MinPositionX)
            {
                newPositionX = 0.0F;
            }

            entity.PositionX = newPositionX;

            return newPositionX;
        }

        public float CalculateHorizontalPositiveMovement(Trolley entity)
        {
            var timePast = (float)ReturnStopwatchvalue();
            var currentSpeed = entity.Speed;
            var travelledDistance = currentSpeed * timePast;

            float newPositionX = entity.PositionX + travelledDistance;

            if (newPositionX > entity.MaxPositionX)
            {
                newPositionX = 136.0F;
            }

            entity.PositionX = newPositionX;

            return newPositionX;
        }

        public void ResetStopWatch()
        {
            _trolleyMoveStopwatch.Reset();
        }

        public void StartStopwatch()
        {
            _trolleyMoveStopwatch.Start();
        }

        public void StopStopwatch()
        {
            _trolleyMoveStopwatch.Stop();
        }

        public double ReturnStopwatchvalue()
        {
            return (double)(_trolleyMoveStopwatch.ElapsedMilliseconds) / 1000;
        }
    }
}

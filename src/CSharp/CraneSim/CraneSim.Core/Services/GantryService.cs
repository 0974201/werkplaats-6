using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using HiveMQtt.Client.Events;

namespace CraneSim.Core.Services
{
    public class GantryService : IGantryService
    {
        public readonly Stopwatch _gantryMoveStopwatch;

        public GantryService()
        {
            _gantryMoveStopwatch = new Stopwatch();
        }

        /* gantry pos calc */

        public float CalculateAcceleration(Gantry entity)
        {
            var accelTime = entity.AccelAndDecelTime;
            var currentSpeed = entity.Speed;
            var topSpeed = entity.MaximumSpeedValue;

            float result = (topSpeed - currentSpeed) / accelTime;

            entity.Acceleration = result;
            return result;
        }

        public float CalculateCurrentSpeed(Gantry entity)
        {
            var timePassed = (float)ReturnStopwatchvalue();

            if (timePassed < entity.AccelAndDecelTime)
            {
                entity.Speed = entity.Acceleration * timePassed;
            }
            else
            {
                entity.Speed = entity.MaximumSpeedValue;
            }

            entity.Speed = Math.Min(entity.MaximumSpeedValue, entity.Speed);

            return entity.Speed;
        }

        public float CalculateNegativeMovement(Gantry entity)
        {
            var timePassed = (float)ReturnStopwatchvalue();
            var currentSpeed = entity.Speed;
            var travelledDist = currentSpeed * timePassed;

            float newPosZ = entity.PositionZ - travelledDist;

            if (newPosZ < entity.MinPosZ)
            {
                newPosZ = 0.0F;
            }

            entity.PositionZ = newPosZ;

            return newPosZ;
        }

        public float CalculatePositiveMovement(Gantry entity)
        {
            var timePassed = (float)ReturnStopwatchvalue();
            var currentSpeed = entity.Speed;
            var travelledDist = currentSpeed * timePassed;

            float newPosZ = entity.PositionZ + travelledDist;

            if (newPosZ > entity.MaxPosZ)
            {
                newPosZ = 1000.0F;
            }

            entity.PositionZ = newPosZ;

            return newPosZ;
        }

        public void ResetStopWatch()
        {
            _gantryMoveStopwatch.Reset();
        }

        public double ReturnStopwatchvalue()
        {
            return (double)(_gantryMoveStopwatch.ElapsedMilliseconds) / 1000;
        }

        public Task SendMessage()
        {
            throw new NotImplementedException();
        }

        public void StartStopwatch()
        {
            _gantryMoveStopwatch.Start();
        }

        public void StopStopwatch()
        {
            _gantryMoveStopwatch.Stop();
        }

        /* mqtt cnx */

        public void Client_AfterConnect(object sender, AfterConnectEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Client_BeforeConnect(object sender, BeforeConnectEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Client_OnDisconnectRecieved(object sender, OnDisconnectReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task DisconnectBrokerConnection()
        {
            throw new NotImplementedException();
        }

        public Task EstablishBrokerConnection()
        {
            throw new NotImplementedException();
        }
    }
}

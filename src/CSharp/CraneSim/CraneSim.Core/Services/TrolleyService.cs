using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using HiveMQtt.Client.Options;
using HiveMQtt.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using HiveMQtt.Client.Events;
using CraneSim.Core.Dtos;
using System.Text.Json;

namespace CraneSim.Core.Services
{
    public class TrolleyService : ITrolleyService
    {
        public readonly Stopwatch _trolleyMoveStopwatch;

        HiveMQClient _client;
        HiveMQClientOptions _options;
        Trolley _activeTrolley;
        public TrolleyService(Trolley activeTrolley)
        {
            _trolleyMoveStopwatch = new Stopwatch();

            //=====================================================================

            _options = new HiveMQClientOptions();
            _options.Host = "c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud";
            _options.Port = 8883;
            _options.UseTLS = true;
            _options.UserName = "cranemqtt";
            _options.Password = "7va@tWTv2.Jw2yk";

            _client = new HiveMQClient(_options);

            _activeTrolley = activeTrolley;

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

        //=====================================================================

        public async Task EstablishBrokerConnection()
        {
            var connectResult = await _client.ConnectAsync().ConfigureAwait(false);

            //subscribe
            await _client.SubscribeAsync("").ConfigureAwait(false);

            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnDisconnectReceived += Client_OnDisconnectRecieved;
            _client.BeforeConnect += Client_BeforeConnect;
            _client.AfterConnect += Client_AfterConnect;
        }

        public async Task DisconnectBrokerConnection()
        {
            await _client.DisconnectAsync().ConfigureAwait(false);
        }

        public async Task SendMessage()
        {
            // Publish
            await _client.PublishAsync("", "").ConfigureAwait(false);
        }

        public void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e)
        {
            //if you have recieved a message you can find it here, and do stuff when OnMessageRecieved.
            var payload = e.PublishMessage.PayloadAsString;
            TrolleyRequestDto trolleyRequestDto = JsonSerializer.Deserialize<TrolleyRequestDto>(payload);

            _activeTrolley.IsActive = trolleyRequestDto.IsActive;
            _activeTrolley.Name = trolleyRequestDto.Name;
            _activeTrolley.Speed = trolleyRequestDto.Speed;
            _activeTrolley.Acceleration = trolleyRequestDto.Acceleration;
            _activeTrolley.PositionX = trolleyRequestDto.PositionX;
        }

        public void Client_OnDisconnectRecieved(object sender, OnDisconnectReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Client_BeforeConnect(object sender, BeforeConnectEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Client_AfterConnect(object sender, AfterConnectEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

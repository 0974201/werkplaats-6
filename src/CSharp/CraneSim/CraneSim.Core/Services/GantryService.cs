using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CraneSim.Core.Dtos.Gantry;
using CraneSim.Core.Dtos.Trolley;
using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using CraneSim.Dtos.Gantry;
using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;

namespace CraneSim.Core.Services
{
    public class GantryService : IGantryService
    {
        public readonly Stopwatch _gantryMoveStopwatch;

        HiveMQClient _client;
        HiveMQClientOptions _options;
        public readonly Gantry _activeGantry;

        public GantryService(Gantry activeGantry)
        {
            _gantryMoveStopwatch = new Stopwatch();

            /* mqtt cred */
            _options = new HiveMQClientOptions();
            _options.Host = "c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud";
            _options.Port = 8883;
            _options.UseTLS = true;

            _options.UserName = "gantrymqtt"; //ik heb dit liever in een .env staan ;-;
            _options.Password = "xC7gqKU6F!GZ#qM";

            _client = new HiveMQClient(_options);

            _activeGantry = activeGantry;
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

        public void StartStopwatch()
        {
            _gantryMoveStopwatch.Start();
        }

        public void StopStopwatch()
        {
            _gantryMoveStopwatch.Stop();
        }

        /* mqtt cnx */
        
        public async Task EstablishBrokerConnection()
        {
            var connectResult = await _client.ConnectAsync().ConfigureAwait(false);

            await _client.SubscribeAsync("gantry/state.command").ConfigureAwait(false);

            _client.OnMessageReceived += Client_OnMessageReceived;
        }

        public async Task DisconnectBrokerConnection()
        {
            bool disconnectResult = await _client.DisconnectAsync().ConfigureAwait(false);
        }

        public async Task SendMessage()
        {
            GantryResponseDto gantryResponse = new GantryResponseDto
            {
                Meta = new GantryResponseMetaDto
                {
                    Name = "gantry",
                    IsActive = _activeGantry.IsActive,
                    Topic = "gantry/state"
                },
                Msg = new GantryResponseMsgDto
                {
                    PositionZ = _activeGantry.PositionZ,
                    Speed = new GantryResponseSpeedDto
                    {
                        Acceleration = _activeGantry.Acceleration,
                        Speed = _activeGantry.Speed
                    }
                }
            };

            string GantryData = JsonSerializer.Serialize(gantryResponse);

            await _client.PublishAsync("gantry/state", GantryData).ConfigureAwait(false);
        }
        
        public void Client_AfterConnect(object sender, AfterConnectEventArgs e)
        {
            _client.AfterConnect += Client_AfterConnect;
            void Client_AfterConnect(object sender, AfterConnectEventArgs e)
            {
                //if you have recieved a message you can find it here, and do stuff when OnMessageRecieved.
            }
        }

        public void Client_BeforeConnect(object sender, BeforeConnectEventArgs e)
        {
            _client.BeforeConnect += Client_BeforeConnect;
            void Client_BeforeConnect(object sender, BeforeConnectEventArgs e)
            {
                //do stuff when OnMessageRecieved.
            }
        }

        public void Client_OnDisconnectRecieved(object sender, OnDisconnectReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e)
        {
            var payload = e.PublishMessage.PayloadAsString;
            GantryRequestDto gantryRequestDto = JsonSerializer.Deserialize<GantryRequestDto>(payload);


            if (gantryRequestDto.Msg.Command == "0")
            {
                //beweging stopt
                StopStopwatch();
                ResetStopWatch();
                _activeGantry.Speed = 0.0F;
                _activeGantry.IsActive = false;

                await SendMessage();
            }

            if (gantryRequestDto.Msg.Command == "1")
            {
                //beweging vooruit
                if (_gantryMoveStopwatch.ElapsedMilliseconds == 0.0)
                {
                    StartStopwatch();
                    _activeGantry.IsActive = true;

                    _ = CalculateAcceleration;
                    _ = CalculateCurrentSpeed;
                    _ = CalculatePositiveMovement;
                    _ = ReturnStopwatchvalue();

                    await SendMessage();
                }
                else
                {
                    _activeGantry.IsActive = true;

                    _ = CalculateAcceleration;
                    _ = CalculateCurrentSpeed;
                    _ = CalculatePositiveMovement;
                    _ = ReturnStopwatchvalue();

                    await SendMessage();
                }

            }

            if (gantryRequestDto.Msg.Command == "-1")
            {
                //beweging achteruit
                if (_gantryMoveStopwatch.ElapsedMilliseconds == 0.0)
                {
                    StartStopwatch();
                    _activeGantry.IsActive = true;

                    _ =CalculateAcceleration;
                    _ = CalculateCurrentSpeed;
                    _ = CalculateNegativeMovement;
                    _ = ReturnStopwatchvalue();

                    await SendMessage();
                }
                else
                {
                    _activeGantry.IsActive = true;

                    _ = CalculateAcceleration;
                    _ = CalculateCurrentSpeed;
                    _ = CalculateNegativeMovement;
                    _ = ReturnStopwatchvalue();

                    await SendMessage();
                }
            }
        }
    }
}

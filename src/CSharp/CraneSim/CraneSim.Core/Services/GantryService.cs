using System.Diagnostics;
using System.Text.Json;
using CraneSim.Core.Dtos.Gantry;
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

        HiveMQClient _gantryClient;
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

            _gantryClient = new HiveMQClient(_options);

            _activeGantry = activeGantry;
        }

        /* gantry pos calc */

        public float CalculateAcceleration()
        {
            var accelTime = _activeGantry.AccelAndDecelTime;
            var currentSpeed = _activeGantry.Speed;
            var topSpeed = _activeGantry.MaximumSpeedValue;

            float result = (topSpeed - currentSpeed) / accelTime;

            _activeGantry.Acceleration = result;
            return result;
        }

        public float CalculateCurrentSpeed()
        {
            var timePassed = (float)ReturnStopwatchvalue();

            if (timePassed < _activeGantry.AccelAndDecelTime)
            {
                _activeGantry.Speed = _activeGantry.Acceleration * timePassed;
            }
            else
            {
                _activeGantry.Speed = _activeGantry.MaximumSpeedValue;
            }

            _activeGantry.Speed = Math.Min(_activeGantry.MaximumSpeedValue, _activeGantry.Speed);

            return _activeGantry.Speed;
        }

        public float CalculateNegativeMovement()
        {
            var timePassed = (float)ReturnStopwatchvalue();
            var currentSpeed = _activeGantry.Speed;
            var travelledDist = currentSpeed * timePassed;

            float newPosZ = _activeGantry.PositionZ - travelledDist;

            if (newPosZ < _activeGantry.MinPosZ)
            {
                newPosZ = 0.0F;
            }

            _activeGantry.PositionZ = newPosZ;

            return newPosZ;
        }

        public float CalculatePositiveMovement()
        {
            var timePassed = (float)ReturnStopwatchvalue();
            var currentSpeed = _activeGantry.Speed;
            var travelledDist = currentSpeed * timePassed;

            float newPosZ = _activeGantry.PositionZ + travelledDist;

            if (newPosZ > _activeGantry.MaxPosZ)
            {
                newPosZ = 1000.0F;
            }

            _activeGantry.PositionZ = newPosZ;

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
            var connectResult = await _gantryClient.ConnectAsync().ConfigureAwait(false);

            await _gantryClient.SubscribeAsync("crane/components/gantry/command").ConfigureAwait(false);

            _gantryClient.OnMessageReceived += Client_OnMessageReceived;
        }

        public async Task DisconnectBrokerConnection()
        {
            bool disconnectResult = await _gantryClient.DisconnectAsync().ConfigureAwait(false);
        }

        public async Task SendMessageAsync()
        {
            GantryResponseDto gantryResponse = new GantryResponseDto
            {
                Meta = new GantryResponseMetaDto
                {
                    Component = "gantry",
                    IsActive = _activeGantry.IsActive,
                    Topic = "crane/components/gantry/state"
                },
                Msg = new GantryResponseMsgDto
                {
                    PositionZ = _activeGantry.PositionZ,
                    Speed = new GantryResponseSpeedDto
                    {
                        ActiveAcceleration = _activeGantry.IsActive,
                        Acceleration = _activeGantry.Acceleration,
                        Speed = _activeGantry.Speed
                    }
                }
            };

            string gantryData = JsonSerializer.Serialize(gantryResponse);

            await _gantryClient.PublishAsync("crane/components/gantry/state", gantryData).ConfigureAwait(false);
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

                await SendMessageAsync();
            }

            if (gantryRequestDto.Msg.Command == "1")
            {
                //beweging vooruit
                if (_gantryMoveStopwatch.ElapsedMilliseconds == 0.0)
                {
                    StartStopwatch();
                    _activeGantry.IsActive = true;

                    var acceleration = CalculateAcceleration();
                    var speed = CalculateCurrentSpeed();
                    var positiveMovement = CalculatePositiveMovement();
                    var stopwatch = ReturnStopwatchvalue();

                    await SendMessageAsync();
                }
                else
                {
                    _activeGantry.IsActive = true;

                    _ = CalculateAcceleration();
                    _ = CalculateCurrentSpeed();
                    _ = CalculatePositiveMovement();
                    _ = ReturnStopwatchvalue();

                    await SendMessageAsync();
                }

            }

            if (gantryRequestDto.Msg.Command == "-1")
            {
                //beweging achteruit
                if (_gantryMoveStopwatch.ElapsedMilliseconds == 0.0)
                {
                    StartStopwatch();
                    _activeGantry.IsActive = true;

                    var acceleration = CalculateAcceleration();
                    var speed = CalculateCurrentSpeed();
                    var negativeMovement = CalculateNegativeMovement();
                    var stopwatch = ReturnStopwatchvalue();

                    await SendMessageAsync();
                }
                else
                {
                    _activeGantry.IsActive = true;

                    _ = CalculateAcceleration();
                    _ = CalculateCurrentSpeed();
                    _ = CalculateNegativeMovement();
                    _ = ReturnStopwatchvalue();

                    await SendMessageAsync();
                }
            }
        }
    }
}

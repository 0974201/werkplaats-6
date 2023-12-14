using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using HiveMQtt.Client.Options;
using HiveMQtt.Client;
using System.Diagnostics;
using HiveMQtt.Client.Events;
using System.Text.Json;
using CraneSim.Core.Dtos.Trolley;

namespace CraneSim.Core.Services
{
    public class TrolleyService : ITrolleyService
    {
        public static readonly Stopwatch _trolleyMoveStopwatch = new Stopwatch();

        HiveMQClient _client;
        HiveMQClientOptions _options;
        public readonly Trolley _activeTrolley;

        public TrolleyService(Trolley activeTrolley)
        {
            _options = new HiveMQClientOptions();
            _options.Host = "c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud";
            _options.Port = 8883;
            _options.UseTLS = true;
            _options.UserName = "cranemqtt";
            _options.Password = "7va@tWTv2.Jw2yk";

            _client = new HiveMQClient(_options);

            _activeTrolley = activeTrolley;

        }

        public float CalculateConstantAccelaration()
        {
            var accelerationTime = _activeTrolley.AccelAndDecelarationTime;
            var currentSpeed = _activeTrolley.Speed;
            var topspeed = _activeTrolley.MaximumSpeedValue;

            float result = (topspeed - currentSpeed) / accelerationTime;

            _activeTrolley.Acceleration = result;
            return result;
        }

        public float CalculateCurrentSpeed()
        {
            var timePast = (float)ReturnStopwatchvalue();

            if (timePast < _activeTrolley.AccelAndDecelarationTime)
            {
                _activeTrolley.Speed = _activeTrolley.Acceleration * timePast;
            }
            else
            {
                _activeTrolley.Speed = _activeTrolley.MaximumSpeedValue;
            }

            _activeTrolley.Speed = Math.Min(_activeTrolley.MaximumSpeedValue, _activeTrolley.Speed);

            return _activeTrolley.Speed;
        }

        public float CalculateHorizontalNegativeMovement()
        {

            var timePast = (float)ReturnStopwatchvalue();
            var currentSpeed = _activeTrolley.Speed;
            var travelledDistance = currentSpeed * timePast;

            float newPositionX = _activeTrolley.PositionX - travelledDistance;

            if (newPositionX < _activeTrolley.MinPositionX)
            {
                newPositionX = _activeTrolley.MinPositionX;
            }

            _activeTrolley.PositionX = newPositionX;

            return newPositionX;
        }

        public float CalculateHorizontalPositiveMovement()
        {
            var timePast = (float)ReturnStopwatchvalue();
            var currentSpeed = _activeTrolley.Speed;
            var travelledDistance = currentSpeed * timePast;

            float newPositionX = _activeTrolley.PositionX + travelledDistance;

            if (newPositionX > _activeTrolley.MaxPositionX)
            {
                newPositionX = _activeTrolley.MaxPositionX;
            }

            _activeTrolley.PositionX = newPositionX;

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

        //============== ⬇⬇⬇⬇ Mqtt code ⬇⬇⬇⬇ ==================

        public async Task EstablishBrokerConnection()
        {
            var connectResult = await _client.ConnectAsync().ConfigureAwait(false);

            // Subscribe
            await _client.SubscribeAsync("crane/components/trolley/command").ConfigureAwait(false);

            _client.OnMessageReceived += Client_OnMessageReceived;
        }

        public async Task DisconnectBrokerConnection()
        {
            bool disconnectResult = await _client.DisconnectAsync().ConfigureAwait(false);
        }

        public async Task SendMessageAsync()
        {
            // Publish
            TrolleyResponseDto trolleyResponse = new TrolleyResponseDto
            {
                Meta = new TrolleyResponseMetaDto
                {
                    Component = "trolley",
                    IsActive = _activeTrolley.IsActive,
                    Topic = "crane/components/trolley/state"
                },
                Msg = new TrolleyResponseMsgDto
                {
                    RelativePosition = _activeTrolley.PositionX,
                    Speed = new TrolleyResponseSpeedDto
                    {
                        Acceleration = _activeTrolley.Acceleration,
                        ActiveAcceleration = _activeTrolley.IsActive,
                        Speed = _activeTrolley.Speed
                    }
                }
            };

            string trolleyDataJson = JsonSerializer.Serialize(trolleyResponse);

            await _client.PublishAsync("crane/components/trolley/state", trolleyDataJson).ConfigureAwait(false);
        }

        public async void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e)
        {
           
            var payload = e.PublishMessage.PayloadAsString;
            TrolleyRequestDto trolleyRequestDto = JsonSerializer.Deserialize<TrolleyRequestDto>(payload);


            if (trolleyRequestDto.Msg.Command == "0")
            {
                //beweging stopt
                StopStopwatch();
                ResetStopWatch();
                _activeTrolley.Speed = 0.0F;
                _activeTrolley.IsActive = false;

                await SendMessageAsync();
            }

            if (trolleyRequestDto.Msg.Command == "1")
            {
                //beweging vooruit
                if (_trolleyMoveStopwatch.ElapsedMilliseconds == 0.0)
                {
                    StartStopwatch();
                    _activeTrolley.IsActive = true;

                    _ = CalculateConstantAccelaration();
                    _ = CalculateCurrentSpeed();
                    _ = CalculateHorizontalPositiveMovement();
                    _ = ReturnStopwatchvalue();

                    await SendMessageAsync();
                }
                else
                {
                    _activeTrolley.IsActive = true;

                    _ = CalculateConstantAccelaration();
                    _ = CalculateCurrentSpeed();
                    _ = CalculateHorizontalPositiveMovement();
                    _ = ReturnStopwatchvalue();

                    await SendMessageAsync();
                }

            }



            if (trolleyRequestDto.Msg.Command == "-1")
            {
                //beweging achteruit
                if (_trolleyMoveStopwatch.ElapsedMilliseconds == 0.0)
                {
                    StartStopwatch();
                    _activeTrolley.IsActive = true;

                    _ = CalculateConstantAccelaration();
                    _ = CalculateCurrentSpeed();
                    _ = CalculateHorizontalNegativeMovement();
                    _ = ReturnStopwatchvalue();

                    await SendMessageAsync();
                }
                else
                {
                    _activeTrolley.IsActive = true;

                    _ = CalculateConstantAccelaration();
                    _ = CalculateCurrentSpeed();
                    _ = CalculateHorizontalNegativeMovement();
                    _ = ReturnStopwatchvalue();

                    await SendMessageAsync();
                }
            }
        }

    }
}

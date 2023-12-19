using CraneSim.Core.Dtos.Crane;
using CraneSim.Core.Dtos.Gantry;
using CraneSim.Core.Dtos.Hoist;
using CraneSim.Core.Dtos.Main;
using CraneSim.Core.Dtos.ShipContainer;
using CraneSim.Core.Dtos.Trolley;
using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CraneSim.Core.Services
{
    public class ShipContainerServices : IShipContainerService
    {
        HiveMQClient _clientShipContainer;
        HiveMQClientOptions _options;
        private readonly Shipcontainer _activeShipContainer;

        public ShipContainerServices(Shipcontainer activeShipContainer)
        {
            _options = new HiveMQClientOptions();
            _options.Host = "c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud";
            _options.Port = 8883;
            _options.UseTLS = true;
            _options.UserName = "cranemqtt";
            _options.Password = "7va@tWTv2.Jw2yk";

            _clientShipContainer = new HiveMQClient(_options);

            _activeShipContainer = activeShipContainer;
        }

        public async Task EstablishBrokerConnection()
        {
            var connectResult = await _clientShipContainer.ConnectAsync().ConfigureAwait(false);
            
            // Subscribe
            var trolleyConnection = await _clientShipContainer.SubscribeAsync("crane/components/trolley/state").ConfigureAwait(false);

            var gantryConnection = await _clientShipContainer.SubscribeAsync("crane/components/gantry/state").ConfigureAwait(false);
            
            var craneConnection = await _clientShipContainer.SubscribeAsync("crane/components/hoist/state").ConfigureAwait(false);

            _clientShipContainer.OnMessageReceived += Client_OnMessageReceived;
        }

        public async Task DisconnectBrokerConnection()
        {
            bool disconnectResult = await _clientShipContainer.DisconnectAsync().ConfigureAwait(false);
        }

        public async void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e)
        {
            var payload = e.PublishMessage.PayloadAsString;
            MainResponseDto mainRequestDto = JsonSerializer.Deserialize<MainResponseDto>(payload);

            //_activeShipContainer.IsConnectedToHoist = true; // Enkel om te testen

            if (mainRequestDto.Meta.Component == "trolley")
            {
                TrolleyResponseDto trolleyRequestDto = JsonSerializer.Deserialize<TrolleyResponseDto>(payload);
                if (_activeShipContainer.IsConnectedToHoist)
                {
                    _activeShipContainer.PositionX = trolleyRequestDto.Msg.RelativePosition;
                    await SendMessageAsync();
                }
            }

            if (mainRequestDto.Meta.Component == "hoist")
            {
                HoistResponseDto hoistRequestDto = JsonSerializer.Deserialize<HoistResponseDto>(payload);
                if (hoistRequestDto.Msg.IsConnected)
                {
                    _activeShipContainer.IsConnectedToHoist = true;
                    _activeShipContainer.PositionY = hoistRequestDto.Msg.RelativePosition.PositionY;
                    await SendMessageAsync();
                }
                else
                {
                    _activeShipContainer.IsConnectedToHoist = false;
                }
                
            }

            if (mainRequestDto.Meta.Component == "gantry")
            {
                GantryResponseDto gantryRequestDto = JsonSerializer.Deserialize<GantryResponseDto>(payload);
                if (_activeShipContainer.IsConnectedToHoist)
                {
                    _activeShipContainer.PositionZ = gantryRequestDto.Msg.PositionZ;
                    await SendMessageAsync();
                }
                
            }
        }

        public async Task SendMessageAsync()
        {
            // Publish
            ShipContainerResponseDto shipContainerResponse = new ShipContainerResponseDto
            {
                Meta = new ShipContainerResponseMetaDto
                {
                    Component = "container",
                    Id = _activeShipContainer.Id,
                    Topic = $"containers/{_activeShipContainer.Id}/state"
                },
                Msg = new ShipContainerResponseMsgDto
                {
                    IsConnected = true,
                    ShipContAbsolPos = new ShipContainerResponseAbsolPosDto
                    {
                        PositionX = _activeShipContainer.PositionX,
                        PositionY = _activeShipContainer.PositionY,
                        PositionZ = _activeShipContainer.PositionZ
                    },
                    Speed = new ShipContainerResponseSpeedDto
                    {
                        Speed = new ShipContainerResponseSubSpeedDto
                        {
                            PositionX = _activeShipContainer.PositionX,
                            PositionY = _activeShipContainer.PositionY,
                            PositionZ = _activeShipContainer.PositionZ
                        }
                    }
                }
            };

            string shipContainerDataJson = JsonSerializer.Serialize(shipContainerResponse);

            await _clientShipContainer.PublishAsync("containers/1/state", shipContainerDataJson).ConfigureAwait(false);
        }
    }
}

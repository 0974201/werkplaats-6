using CraneSim.Core.Dtos.Crane;
using CraneSim.Core.Dtos.Hoist;
using CraneSim.Core.Dtos.Main;
using CraneSim.Core.Dtos.ShipContainer;
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
        HiveMQClient _client;
        HiveMQClientOptions _options;
        private readonly Shipcontainer _activeShipContainer;

        public ShipContainerServices(Shipcontainer activeShipContainer)
        {
            _options = new HiveMQClientOptions();
            _options.Host = "c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud";
            _options.Port = 8883;
            _options.UseTLS = true;
            _options.UserName = "craneMqtt"; // to check with hivemq
            _options.Password = "7va@tWTv2.Jw2yk";

            _client = new HiveMQClient(_options);

            _activeShipContainer = activeShipContainer;
        }

        public async Task EstablishBrokerConnection()
        {
            var connectResult = await _client.ConnectAsync().ConfigureAwait(false);

            // Subscribe
            await _client.SubscribeAsync("crane/components/gantry/command").ConfigureAwait(false);
            await _client.SubscribeAsync("crane/components/trolley/command").ConfigureAwait(false);
            await _client.SubscribeAsync("crane/components/hoist/command").ConfigureAwait(false);

            _client.OnMessageReceived += Client_OnMessageReceived;
        }

        public async Task DisconnectBrokerConnection()
        {
            bool disconnectResult = await _client.DisconnectAsync().ConfigureAwait(false);
        }

        public async void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e)
        {
            var payload = e.PublishMessage.PayloadAsString;
            MainRequestDto mainRequestDto = JsonSerializer.Deserialize<MainRequestDto>(payload);

            if (mainRequestDto.Meta.Component == "trolley")
            {
                //TrolleyRequestDto trolleyRequestDto = JsonSerializer.Deserialize<MainRequestDto>(payload);
                _activeShipContainer.PositionX = 0.0F;
            }

            if (mainRequestDto.Meta.Component == "hoist")
            {
                HoistRequestDto hoistRequestDto = JsonSerializer.Deserialize<HoistRequestDto>(payload);
                _activeShipContainer.PositionY = 0.0F;
            }

            if (mainRequestDto.Meta.Component == "gantry")
            {
                GantryRequestDto gantryRequestDto = JsonSerializer.Deserialize<GantryRequestDto>(payload);
                _activeShipContainer.PositionZ = 0.0F;
            }

            await SendMessageAsync();

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

            await _client.PublishAsync("crane/components/trolley/state", shipContainerDataJson).ConfigureAwait(false);
        }
    }
}

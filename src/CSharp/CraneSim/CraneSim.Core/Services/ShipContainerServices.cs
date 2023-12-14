using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _options.UserName = "ShipContainerMqtt"; // to check with hivemq
            _options.Password = "7va@tWTv2.Jw2yk";

            _client = new HiveMQClient(_options);

            _activeShipContainer = activeShipContainer;
        }

        public Task EstablishBrokerConnection()
        {
            throw new NotImplementedException();
        }

        public async Task DisconnectBrokerConnection()
        {
            bool disconnectResult = await _client.DisconnectAsync().ConfigureAwait(false);
        }

        public void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task SendMessage()
        {
            throw new NotImplementedException();
        }
    }
}

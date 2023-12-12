using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using CraneSim.Core.Dtos;

namespace CraneSim.Infrastructure.MicroServices
{
    public class TrolleyMicroService : ITrolleyMicroSevice
    {
        HiveMQClient _client;
        HiveMQClientOptions _options;
        Trolley _activeTrolley;

        public TrolleyMicroService(Trolley activeTrolley)
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

        public async Task SendMessage()
        {
            //publish

            await _client.PublishAsync("", "").ConfigureAwait(false);
        }

        public Task DisconnectBrokerConnection()
        {
            throw new NotImplementedException();
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
            //if you have recieved a message you can find it here, and do stuff when OnMessageRecieved.
        }

        public void Client_BeforeConnect(object sender, BeforeConnectEventArgs e)
        {
            //do stuff when OnMessageRecieved.
        }

        public void Client_AfterConnect(object sender, AfterConnectEventArgs e)
        {
            //if you have recieved a message you can find it here, and do stuff when OnMessageRecieved.
        }

        
    }
}

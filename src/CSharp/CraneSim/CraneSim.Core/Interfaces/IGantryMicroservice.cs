using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiveMQtt.Client.Events;

namespace CraneSim.Core.Interfaces
{
    public interface IGantryMicroservice
    {
        Task EstablishBrokerConnection();
        Task SendMessage();
        Task DisconnectBrokerConnection();
        
        void Client_BeforeConnect(object sender, BeforeConnectEventArgs e);
        void Client_AfterConnect(object sender, AfterConnectEventArgs e);
        void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e);
        void Client_OnDisconnectRecieved(object sender, OnDisconnectReceivedEventArgs e);
    }
}

using HiveMQtt.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Core.Interfaces
{
    public interface IShipContainerService
    {
        //=========== ⬇⬇⬇⬇ Mqtt code ⬇⬇⬇⬇ ===============

        Task EstablishBrokerConnection();
        Task DisconnectBrokerConnection();
        Task SendMessageAsync();
        void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e);
    }
}

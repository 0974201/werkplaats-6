using CraneSim.Core.Entities;
using HiveMQtt.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraneSim.Core.Interfaces
{
    public interface ITrolleyService
    {
        float CalculateHorizontalPositiveMovement();
        float CalculateHorizontalNegativeMovement();
        float CalculateCurrentSpeed();
        float CalculateConstantAccelaration();
        void ResetStopWatch();
        void StartStopwatch();
        void StopStopwatch();
        double ReturnStopwatchvalue();

        //=========== ⬇⬇⬇⬇ Mqtt code ⬇⬇⬇⬇ ===============

        Task EstablishBrokerConnection();
        Task DisconnectBrokerConnection();
        Task SendMessageAsync();
        void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e);
        
    }
}

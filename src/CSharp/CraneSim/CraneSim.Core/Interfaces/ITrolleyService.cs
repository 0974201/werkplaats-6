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
        float CalculateHorizontalPositiveMovement(Trolley entity);
        float CalculateHorizontalNegativeMovement(Trolley entity);
        float CalculateCurrentSpeed(Trolley entity);
        float CalculateConstantAccelaration(Trolley entity);
        void ResetStopWatch();
        void StartStopwatch();
        void StopStopwatch();
        double ReturnStopwatchvalue();

        //===================================================================
        Task EstablishBrokerConnection();
        Task DisconnectBrokerConnection();
        Task SendMessage();
        void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e);
        void Client_OnDisconnectRecieved(object sender, OnDisconnectReceivedEventArgs e);
        void Client_BeforeConnect(object sender, BeforeConnectEventArgs e);
        void Client_AfterConnect(object sender, AfterConnectEventArgs e);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CraneSim.Core.Entities;
using HiveMQtt.Client.Events;

namespace CraneSim.Core.Interfaces
{
    internal interface IGantryService
    {
        /* calculate pos */

        float CalculatePositiveMovement(Gantry entity);
        float CalculateNegativeMovement(Gantry entity);
        float CalculateCurrentSpeed(Gantry entity);
        float CalculateAcceleration(Gantry entity);
        void ResetStopWatch();
        void StartStopwatch();
        void StopStopwatch();
        double ReturnStopwatchvalue();
        
        /* mqtt cnx */
        
        Task EstablishBrokerConnection();
        Task SendMessage();
        Task DisconnectBrokerConnection();

        void Client_BeforeConnect(object sender, BeforeConnectEventArgs e);
        void Client_AfterConnect(object sender, AfterConnectEventArgs e);
        void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e);
        void Client_OnDisconnectRecieved(object sender, OnDisconnectReceivedEventArgs e);
    }
}

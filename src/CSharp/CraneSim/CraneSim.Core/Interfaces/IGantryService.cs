using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CraneSim.Core.Entities;
using HiveMQtt.Client.Events;

namespace CraneSim.Core.Interfaces
{
    public interface IGantryService
    {
        /* calculate pos */

        float CalculatePositiveMovement();
        float CalculateNegativeMovement();
        float CalculateCurrentSpeed();
        float CalculateAcceleration();
        void ResetStopWatch();
        void StartStopwatch();
        void StopStopwatch();
        double ReturnStopwatchvalue();
        
        /* mqtt cnx */
        
        Task EstablishBrokerConnection();
        Task SendMessageAsync();
        Task DisconnectBrokerConnection();

        void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e);
        
    }
}

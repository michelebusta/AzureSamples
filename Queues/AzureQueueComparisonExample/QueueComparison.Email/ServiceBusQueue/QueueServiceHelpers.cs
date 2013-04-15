using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace MovieDb.Shared.ServiceBus
{
    public static class QueueServiceHelpers
    {
        public static ServiceEndpoint CreateSbQueueEndpoint(Type t, TokenProvider tkProvider, string sbAddress, TimeSpan receiveTimeout)
        {
            TransportClientEndpointBehavior behaviour = new TransportClientEndpointBehavior(tkProvider);

            NetMessagingBinding binding = new NetMessagingBinding();
            binding.PrefetchCount = -1;
            binding.ReceiveTimeout = new TimeSpan(0, 20, 0); // default 20 mins for long polling

            EndpointAddress address = new EndpointAddress(sbAddress);

            ContractDescription desc = ContractDescription.GetContract(t);

            ServiceEndpoint endpoint = new ServiceEndpoint(desc, binding, address);
            endpoint.Behaviors.Add(behaviour);

            return endpoint;
        }
    }
}

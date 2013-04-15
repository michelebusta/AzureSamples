using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieDb.Shared;
using System.ServiceModel;
using Microsoft.ServiceBus.Messaging;
using MovieDb.Shared.Smtp;

namespace MovieDb
{
    public class EmailServiceBusQueueEngine : IEmailRequest
    {

        [ReceiveContextEnabled]
        public void OnEmailRequest(string emailContent)
        {
            try
            {
                var incomingProperties = OperationContext.Current.IncomingMessageProperties;
                var property = incomingProperties[BrokeredMessageProperty.Name] as BrokeredMessageProperty;

                EmailUtility.SendSearchNotificationEmail(emailContent, "Email queued with Service Bus Queue");    
            }
            catch 
            {
                
            }
        }
    }
}
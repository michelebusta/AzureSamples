using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus.Messaging;
using System.ServiceModel.Channels;
using Microsoft.ServiceBus;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Diagnostics;
using QueueComparison.Web.Shared;
using System.Net;
using SendGridMail.Transport;
using SendGridMail;
using System.Net.Mail;

namespace MovieDb.Shared.Smtp
{
    public static class EmailUtility
    {
       
        public static void SendSearchNotificationToQueue(string emailAddress)
        {
            bool useServiceBus = bool.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_EnableServiceBusQueue"));
            bool useAzureStorage = bool.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_EnableStorageQueue"));

            if (useServiceBus)
            {
                QueueEmailServiceBus(emailAddress);
                Trace.TraceInformation("Sending email to: " + emailAddress);
            }

            if (useAzureStorage)
            {
                QueueEmailStorage(emailAddress);
                Trace.TraceInformation("Sending email to: " + emailAddress);
            }

            if (useServiceBus == false && useAzureStorage == false)
            {
                Trace.TraceError("Both queues are turned off!");
            }
            else if (useServiceBus == true && useAzureStorage == true)
            {
                Trace.TraceWarning("Both queues are turned on");

            }
        }

        public static void SendSearchNotificationEmail(string toAddress, string subject)
        {
            string fromAddress = RoleEnvironment.GetConfigurationSettingValue("SmtpFromAddress");
            string fromName = "QueueDemo";
            string emailContent = "This is an email!";

            SendEmailInfo info = new SendEmailInfo();
            info.Subject = subject;
            info.FromEmail = fromAddress;
            info.FromName = fromName;

            string smtpService = RoleEnvironment.GetConfigurationSettingValue("SmtpService"); // should be "SendGrid" or "Azure"

            if (smtpService == "SendGrid")
            {
                info.Host = RoleEnvironment.GetConfigurationSettingValue("SendGridHost");
                info.Port = int.Parse(RoleEnvironment.GetConfigurationSettingValue("SendGridPort"));
            }
            else if (smtpService == "Azure")
            {
                info.Host = RoleEnvironment.GetConfigurationSettingValue("SmtpOutAddress");
                info.Port = int.Parse(RoleEnvironment.GetConfigurationSettingValue("SmtpOutPort"));
            } 
            
            info.ToEmail = toAddress;
            info.ToName = toAddress;
            info.TextContent = emailContent;

            SendEmail(info); 
        }

        private static void QueueEmailStorage(string emailAddress)
        {
            var queueClient = GetQueueClient();

            QueueEmailStorageQueue(emailAddress);
        }

        private static void QueueEmailStorageQueue(string emailAddress)
        {
            // get client to interact with service
            var client = GetQueueClient();
            //get queue object
            CloudQueue cloudQueue = client.GetQueueReference(RoleEnvironment.GetConfigurationSettingValue("StorageQueueName"));
            //create that if we dont have it, so we can hve dynamic queue

            if (!cloudQueue.Exists())
            {
                cloudQueue.Create();
                cloudQueue.FetchAttributes();
                //you can add metadata
                cloudQueue.Metadata.Add("CreatedOn", DateTime.UtcNow.ToString());
                cloudQueue.SetMetadata();
            }

            //set message
            CloudQueueMessage qmessage = new CloudQueueMessage(emailAddress);
            cloudQueue.AddMessage(qmessage);

        }

        private static void SendEmail(string fromAddress, string fromName, string toAddress, string htmlBody, string textBody, string subject)
        {
            try
            {
                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                mailMsg.To.Add(new System.Net.Mail.MailAddress(toAddress, ""));
                mailMsg.From = new System.Net.Mail.MailAddress(fromAddress, fromName);
                mailMsg.Subject = subject;

                if (String.IsNullOrEmpty(textBody) && String.IsNullOrEmpty(htmlBody))
                {
                    //DiagnosticsHelper.Throw("Email cannot be empty.");
                }

                if (!String.IsNullOrEmpty(htmlBody))
                {
                    mailMsg.AlternateViews.Add(System.Net.Mail.AlternateView.CreateAlternateViewFromString(htmlBody, null, System.Net.Mime.MediaTypeNames.Text.Html));
                }

                if (!String.IsNullOrEmpty(textBody))
                {
                    mailMsg.AlternateViews.Add(System.Net.Mail.AlternateView.CreateAlternateViewFromString(textBody, null, System.Net.Mime.MediaTypeNames.Text.Plain));
                }

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(RoleEnvironment.GetConfigurationSettingValue("SmtpOutAddress"), Int32.Parse(RoleEnvironment.GetConfigurationSettingValue("SmtpOutPort")));
                smtpClient.Send(mailMsg);

            }
            catch 
            {
                // don't really want end user to see the error if we can't send email here
            }
        }

        private static void QueueEmailServiceBus(string emailContent)
        {
            ChannelFactory<IEmailRequest> factory = null;
            try
            {
                string sbNamespace = RoleEnvironment.GetConfigurationSettingValue("ServiceBusNamespace");
                string sbIssuer = RoleEnvironment.GetConfigurationSettingValue("ServiceBusIssuer");
                string sbKey = RoleEnvironment.GetConfigurationSettingValue("ServiceBusKey");
                string sbQueue = RoleEnvironment.GetConfigurationSettingValue("ServiceBusQueue");

                string sbAddress = String.Format("sb://{0}.servicebus.windows.net/{1}", sbNamespace, sbQueue);

                TokenProvider tkProvider = TokenProvider.CreateSharedSecretTokenProvider(sbIssuer, sbKey);

                TransportClientEndpointBehavior behaviour = new TransportClientEndpointBehavior(tkProvider);

                NetMessagingBinding binding = new NetMessagingBinding();
                binding.PrefetchCount = -1;

                EndpointAddress address = new EndpointAddress(sbAddress);

                ContractDescription desc = ContractDescription.GetContract(typeof(IEmailRequest));
                ServiceEndpoint endpoint = new ServiceEndpoint(desc, binding, address);
                endpoint.Behaviors.Add(behaviour);

                factory = new ChannelFactory<IEmailRequest>(endpoint);
                var clientChannel = factory.CreateChannel();
                ((IChannel)clientChannel).Open();

                    AddToServiceBusQueue(emailContent, clientChannel);

                // close sender
                ((IChannel)clientChannel).Close();
                factory.Close();
            }
            catch 
            {
                // TODO: this can't be an exception
                // this needs to log that we did not queue the thumbnail, so we can retry it later
            }
        }

        private static CloudQueueClient GetQueueClient()
        {
            var account = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageQueueConnection"));

            return account.CreateCloudQueueClient();
        }


        private static void AddToServiceBusQueue(string emailContent, IEmailRequest clientChannel)
        {
            // use operationContext to change BrokeredMessageProperties if needed.
            using (var scope = new OperationContextScope((IContextChannel)clientChannel))
            {
                // Create a new BrokeredMessageProperty object
                var property = new BrokeredMessageProperty();

                // Use the BrokeredMessageProperty object to set the BrokeredMessage properties
                property.Label = "EmailRequest";
                property.MessageId = Guid.NewGuid().ToString();

                // Add BrokeredMessageProperty to the OutgoingMessageProperties bag provided 
                // by the current Operation Context 
                OperationContext.Current.OutgoingMessageProperties.Add(BrokeredMessageProperty.Name, property);

                clientChannel.OnEmailRequest(emailContent);
            }
        }

      
        

        internal static bool SendEmail(SendEmailInfo emailInfo)
        {
            bool emailSent = false;


            string smtpService = RoleEnvironment.GetConfigurationSettingValue("SmtpService"); // should be "SendGrid" or "Azure"

            if (smtpService == "SendGrid")
            {
                DiagnosticsHelper.TraceDebug("Sending email using SendGrid");
                emailSent = SendEmailWithSendGrid(emailInfo);
            }
            else if (smtpService == "Azure")
            {
                DiagnosticsHelper.TraceDebug("Sending email using Azure");
                emailSent = SendEmailWithAzure(emailInfo);
            }
            else
            {
                DiagnosticsHelper.ThrowAdmin("Invalid configuration for SmtpService setting. Should be SendGrid or Azure");
            }

            return emailSent;
        }
        internal static bool SendEmailWithSendGrid(SendEmailInfo emailInfo)
        {
            bool emailSent = false;

            try
            {
                // Setup the email properties.
                var from = new MailAddress(emailInfo.FromEmail);
                var to = new MailAddress[] { new MailAddress(emailInfo.ToEmail) };
                var cc = new MailAddress[] { };
                var bcc = new MailAddress[] { };
                var subject = emailInfo.Subject;
                var html = emailInfo.HtmlContent;
                var text = emailInfo.TextContent;
                var transport = SendGridMail.TransportType.SMTP;

                // Create an email, passing in the the eight properties as arguments.
                SendGrid myMessage = SendGrid.GenerateInstance(from, to, cc, bcc, subject, html, text, transport);

                // Create network credentials to access your SendGrid account.
                var username = RoleEnvironment.GetConfigurationSettingValue("SendGridUser");
                var pswd = RoleEnvironment.GetConfigurationSettingValue("SendGridPassword");

                var credentials = new NetworkCredential(username, pswd);


                var transportSMTP = SMTP.GenerateInstance(credentials); // defaults to smtp.sendgrid.net and 25

                // Send the email.
                transportSMTP.Deliver(myMessage);

                emailSent = true;
            }
            catch (Exception ex)
            {
                DiagnosticsHelper.Throw("Unable to send email. Please try again later.", ex);
            }
            return emailSent;
        }

        internal static bool SendEmailWithAzure(SendEmailInfo emailInfo)
        {
            bool emailSent = false;

            try
            {


                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                mailMsg.To.Add(new System.Net.Mail.MailAddress(emailInfo.ToEmail, emailInfo.ToName));
                mailMsg.From = new System.Net.Mail.MailAddress(emailInfo.FromEmail, emailInfo.FromName);
                mailMsg.Subject = emailInfo.Subject;


                if (!String.IsNullOrEmpty(emailInfo.HtmlContent))
                {
                    mailMsg.AlternateViews.Add(System.Net.Mail.AlternateView.CreateAlternateViewFromString(emailInfo.HtmlContent, null, System.Net.Mime.MediaTypeNames.Text.Html));
                }

                if (!String.IsNullOrEmpty(emailInfo.TextContent))
                {
                    mailMsg.AlternateViews.Add(System.Net.Mail.AlternateView.CreateAlternateViewFromString(emailInfo.TextContent, null, System.Net.Mime.MediaTypeNames.Text.Plain));
                }

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(emailInfo.Host, emailInfo.Port);
                smtpClient.Send(mailMsg);
                emailSent = true;
            }
            catch (Exception ex)
            {
                DiagnosticsHelper.Throw("Unable to send email. Please try again later.", ex);
            }
            return emailSent;
        }
    }

    internal class SendEmailInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string HtmlContent { get; set; }
        public string TextContent { get; set; }
        public string Subject { get; set; }
    }
        

    
}

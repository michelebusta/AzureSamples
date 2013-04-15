using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;

namespace MovieDb.Shared.ServiceBus
{
    public class QueueUtil
    {
        // Create the NamespaceManager for management operations (queue)
        public static NamespaceManager CreateNamespaceManager(string serviceBusNamespace, string serviceBusIssuerName, string serviceBusIssuerKey)
        {
            // Create SharedSecretCredential object for access control service
            TokenProvider credentials = TokenProvider.CreateSharedSecretTokenProvider(serviceBusIssuerName, serviceBusIssuerKey);

            // Create the management Uri
            Uri managementUri = ServiceBusEnvironment.CreateServiceUri("sb", serviceBusNamespace, string.Empty);
            var namespaceClient = new NamespaceManager(managementUri, credentials);

            return namespaceClient;
        }

        public static QueueDescription CreateQueue(QueueDescription queueDescription, QueueCreateMode createMode, NamespaceManager nsManager)
        {
            // Try deleting the queue before creation. Ignore exception if queue does not exist.
            if (createMode == QueueCreateMode.DeleteIfExists)
            {
                try
                {
                    nsManager.DeleteQueue(queueDescription.Path);
                }
                catch (MessagingEntityNotFoundException)
                {
                }
            }

            try
            {
                return nsManager.CreateQueue(queueDescription);
            }
            catch (MessagingEntityAlreadyExistsException)
            {
                if (createMode != QueueCreateMode.IgnoreIfExists)
                    throw;
                return nsManager.GetQueue(queueDescription.Path);
            }
        }

        // Create the entity (queue)
        public static QueueDescription CreateQueue(string queueName, bool session, QueueCreateMode createMode, NamespaceManager nsManager)
        {
            var queueDescription = new QueueDescription(queueName)
            {
                RequiresSession = session,
            };

            return CreateQueue(queueDescription, createMode, nsManager);
        }

        public static TopicDescription CreateTopic(string topicName, QueueCreateMode createMode, NamespaceManager namespaceClient)
        {
            // Try deleting the topic before creation. Ignore exception if queue does not exist.
            if (createMode == QueueCreateMode.DeleteIfExists)
            {
                try
                {
                    namespaceClient.DeleteTopic(topicName);
                }
                catch (MessagingEntityNotFoundException)
                {
                }
            }
            try
            {
                return namespaceClient.CreateTopic(topicName);
            }
            catch (MessagingEntityAlreadyExistsException)
            {
                if (createMode != QueueCreateMode.IgnoreIfExists)
                    throw;
                return namespaceClient.GetTopic(topicName);
            }

        }

        public static SubscriptionDescription CreateSubscription(string subscriptionName, string topicName, string filterExpression, QueueCreateMode createMode, NamespaceManager namespaceClient)
        {
            // Try deleting the topic before creation. Ignore exception if queue does not exist.
            if (createMode == QueueCreateMode.DeleteIfExists)
            {
                try
                {
                    namespaceClient.DeleteSubscription(topicName, subscriptionName);
                }
                catch (MessagingEntityNotFoundException)
                {
                }
            }
            try
            {
                Filter filter = null;
                if (!String.IsNullOrEmpty(filterExpression))
                {
                    filter = new SqlFilter(filterExpression);
                }
                else
                {
                    filter = new TrueFilter();
                }
                return namespaceClient.CreateSubscription(topicName, subscriptionName, filter);
            }
            catch (MessagingEntityAlreadyExistsException)
            {
                if (createMode != QueueCreateMode.IgnoreIfExists)
                    throw;
                return namespaceClient.GetSubscription(topicName, subscriptionName);
            }
        }
    }
}

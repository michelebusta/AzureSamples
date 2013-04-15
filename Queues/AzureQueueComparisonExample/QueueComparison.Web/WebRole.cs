using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.ServiceModel;
using Microsoft.ServiceBus;
using Microsoft.WindowsAzure.StorageClient;
using System.Threading.Tasks;
using MovieDb.Shared.Smtp;
using System.Threading;
using MovieDb.Shared.ServiceBus;
using MovieDb;
using MovieDb.Shared;
using System.Net;
using QueueComparison.Web.Shared;
using System.Diagnostics;

namespace QueueComparison.Web
{
    public class WebRole : RoleEntryPoint
    {

        ServiceHost host = null;
        NamespaceManager namespaceManager = null;


        public override bool OnStart()
        {
            try
            {

                // Role settings
                RoleEnvironment.Changing +=new EventHandler<RoleEnvironmentChangingEventArgs>(RoleEnvironment_Changing);

                bool disableDiagnostics = bool.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_DisableDiagnostics"));

                if (!disableDiagnostics)
                {
                    ServicePointManager.DefaultConnectionLimit = int.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_ConnectionLimit"));

                    // diagnostics
                    TimeSpan tsLog = TimeSpan.FromMinutes(int.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_LogTransferPeriod"))); // prod sb 15m

                    DiagnosticMonitorConfiguration dmc =                   DiagnosticMonitor.GetDefaultInitialConfiguration();
                    dmc.OverallQuotaInMB = int.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_DiagnosticsQuota"));

                    dmc.Logs.ScheduledTransferPeriod = tsLog;
                    LogLevel level = (LogLevel)Enum.Parse(typeof(LogLevel), RoleEnvironment.GetConfigurationSettingValue("REC_LogLevel"));
                    dmc.Logs.ScheduledTransferLogLevelFilter = level;

                    // directories
                    dmc.Directories.ScheduledTransferPeriod = tsLog;


                    // Azure logs
                    dmc.Logs.ScheduledTransferLogLevelFilter = level;
                    dmc.Logs.ScheduledTransferPeriod = tsLog;

                    // diagnostics logs
                    dmc.DiagnosticInfrastructureLogs.ScheduledTransferLogLevelFilter = level;
                    dmc.DiagnosticInfrastructureLogs.ScheduledTransferPeriod = tsLog;

                    // add event logs
                    dmc.WindowsEventLog.ScheduledTransferLogLevelFilter = level;
                    dmc.WindowsEventLog.ScheduledTransferPeriod = tsLog;
                    dmc.WindowsEventLog.DataSources.Add("Application!*");
                    dmc.WindowsEventLog.DataSources.Add("System!*");

                    // add perf counters
                    dmc.PerformanceCounters.ScheduledTransferPeriod = TimeSpan.FromMinutes(int.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_PerfTransferPeriod"))); // prod sb 5-10m
                    dmc.PerformanceCounters.BufferQuotaInMB = int.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_BufferQuota"));
                    TimeSpan perfSampleRate = TimeSpan.FromSeconds(int.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_PerfSampleRate"))); // prod sb 5m

                    dmc.PerformanceCounters.DataSources.Add(new PerformanceCounterConfiguration()
                    {
                        CounterSpecifier = @"\Memory\Available Bytes",
                        SampleRate = perfSampleRate
                    });

                    dmc.PerformanceCounters.DataSources.Add(new PerformanceCounterConfiguration()
                    {
                        CounterSpecifier = @"\Processor(_Total)\% Processor Time",
                        SampleRate = perfSampleRate
                    });

                    dmc.PerformanceCounters.DataSources.Add(new PerformanceCounterConfiguration()
                    {
                        CounterSpecifier = @"\Network Interface(*)\Bytes Sent/sec",
                        SampleRate = perfSampleRate
                    });

                    dmc.PerformanceCounters.DataSources.Add(new PerformanceCounterConfiguration()
                    {
                        CounterSpecifier = @"\Network Interface(*)\Bytes Total/sec",
                        SampleRate = perfSampleRate
                    });


                    DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", dmc);

                    Trace.TraceInformation("Diagnostics initialized.");
                }
                else
                {
                    Trace.TraceInformation("Diagnostics has been disabled.");

                }

            }
            catch (Exception ex)
            {
                DiagnosticsHelper.ThrowAdmin(string.Format("Exception in worker role execution. {0}", ex.ToString()), ex);
            }

            return base.OnStart();

        
        }

        void RoleEnvironment_Changing(object sender, RoleEnvironmentChangingEventArgs e)
        {
            try
            {
                var result = e.Changes.Where(x => x is RoleEnvironmentConfigurationSettingChange);
                if (result != null && result.Count() > 0)
                {
                    var list = result.ToList();
                    list.ForEach(x =>
                    {
                        var change = x as RoleEnvironmentConfigurationSettingChange;
                        if (change.ConfigurationSettingName.StartsWith("REC_"))
                        {
                            DiagnosticsHelper.TraceWarning(string.Format("Configuration setting change includes at least one setting that requires recycle. Recycling now."));
                            e.Cancel = true; // time to recycle
                        }
                    }
                    );

                }

            }
            catch (Exception ex)
            {
                DiagnosticsHelper.TraceException(string.Format("An exception occured in the RoleEnvironmentChanging event. {0}", ex.Message), ex);
            }

        }



        public override void Run()
        {
            if (bool.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_EnableServiceBusQueue")))
            {
                MonitorServiceBusEmailQueue();
            }

            if (bool.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_EnableStorageQueue")))
            {
                MonitorStorageEmailQueue();
            }
        }

        private void MonitorStorageEmailQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageQueueConnection"));

            // Create the queue client
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference(RoleEnvironment.GetConfigurationSettingValue("StorageQueueName"));

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExist();

           // Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    // do exponential backoff for a production system to save money...
                    int minInterval = int.Parse(RoleEnvironment.GetConfigurationSettingValue("QueueMinInterval"));
                    int interval = minInterval;

                    int exponent = int.Parse(RoleEnvironment.GetConfigurationSettingValue("QueueInteralExponent"));
                    int maxInterval = 60;

                    var msg = queue.GetMessage();
                    if (msg != null)
                    {
                        EmailUtility.SendSearchNotificationEmail(msg.AsString, "Email queued with Azure Storage Queue");

                        queue.DeleteMessage(msg);
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(interval));
                        interval = Math.Min(maxInterval, interval * exponent);
                    }
                }
            }//);
        }

        private void MonitorServiceBusEmailQueue()
        {
            string sbNamespace = RoleEnvironment.GetConfigurationSettingValue("ServiceBusNamespace");
            string sbIssuer = RoleEnvironment.GetConfigurationSettingValue("ServiceBusIssuer");
            string sbKey = RoleEnvironment.GetConfigurationSettingValue("ServiceBusKey");
            string sbQueue = RoleEnvironment.GetConfigurationSettingValue("ServiceBusQueue");

            TokenProvider tkProvider = TokenProvider.CreateSharedSecretTokenProvider(sbIssuer, sbKey);

            string sbAddress = String.Format("sb://{0}.servicebus.windows.net/{1}", sbNamespace, sbQueue);

            // create namespace manager
            namespaceManager = QueueUtil.CreateNamespaceManager(sbNamespace, sbIssuer, sbKey);

            // create queue
            var queueDescription = QueueUtil.CreateQueue(sbQueue, false, QueueCreateMode.IgnoreIfExists, namespaceManager);

            // create the host
            host = new ServiceHost(typeof(EmailServiceBusQueueEngine));

            int receiveTimeout = int.Parse(RoleEnvironment.GetConfigurationSettingValue("REC_SBReceiveTimeout")); 
    
            TimeSpan ts = new TimeSpan(0, receiveTimeout, 0);
            host.AddServiceEndpoint(QueueServiceHelpers.CreateSbQueueEndpoint(typeof(IEmailRequest), tkProvider, sbAddress, ts));

            host.Faulted += host_Faulted;

            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException)
            {
                return;
            }
            catch (Exception ex)
            {
                DiagnosticsHelper.TraceException(ex);
            }

        }

        void host_Faulted(object sender, EventArgs e)
        {
            // log it.
            DiagnosticsHelper.TraceException("Host faulted.");
        }

        public override void OnStop()
        {
            if (host != null)
            {
                try
                {
                    host.Close();
                }
                catch
                {
                    host.Abort();
                }
            }

            base.OnStop();
        }

        
    }
}
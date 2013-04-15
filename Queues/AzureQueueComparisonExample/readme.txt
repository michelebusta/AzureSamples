AzureQueueComparisonExample

This sample illustrates the following:
- Cloud services
- ETW tracing and performance counters set up in the webrole.cs file
- The use of two different queues: service bus or storage queues
- The ability to recycle a role with config setting changes

This samples assumes the following:
- You are using SMTP server for local testing which means you should set up SMTPForDev utility to capture email for convenience.
- You are using SendGrid for remote testing, which means you have to provide settings for SendGrid in the properties for the cloud service.
- You have to provide your service bus account, keys for diagnostics
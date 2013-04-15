<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AzureQueueComparisonExample" generation="1" functional="0" release="0" Id="95f10aca-aa80-4877-a4d8-c63a0f1eac3c" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AzureQueueComparisonExampleGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="QueueComparison.Web:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/LB:QueueComparison.Web:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/LB:QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapCertificate|QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:QueueInteralExponent" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:QueueInteralExponent" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:QueueMinInterval" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:QueueMinInterval" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_BufferQuota" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_BufferQuota" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_ConnectionLimit" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_ConnectionLimit" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_DiagnosticsQuota" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_DiagnosticsQuota" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_DisableDiagnostics" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_DisableDiagnostics" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_EnableServiceBusQueue" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_EnableServiceBusQueue" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_EnableStorageQueue" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_EnableStorageQueue" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_EnableWindowsLogs" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_EnableWindowsLogs" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_LogLevel" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_LogLevel" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_LogTransferPeriod" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_LogTransferPeriod" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_PerfSampleRate" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_PerfSampleRate" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_PerfTransferPeriod" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_PerfTransferPeriod" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:REC_SBReceiveTimeout" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:REC_SBReceiveTimeout" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SendGridHost" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SendGridHost" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SendGridPassword" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SendGridPassword" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SendGridPort" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SendGridPort" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SendGridUser" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SendGridUser" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:ServiceBusIssuer" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:ServiceBusIssuer" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:ServiceBusKey" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:ServiceBusKey" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:ServiceBusNamespace" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:ServiceBusNamespace" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:ServiceBusQueue" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:ServiceBusQueue" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SmtpAdminAddress" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SmtpAdminAddress" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SmtpFromAddress" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SmtpFromAddress" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SmtpOutAddress" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SmtpOutAddress" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SmtpOutPort" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SmtpOutPort" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:SmtpService" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:SmtpService" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:StorageQueueConnection" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:StorageQueueConnection" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.Web:StorageQueueName" defaultValue="">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.Web:StorageQueueName" />
          </maps>
        </aCS>
        <aCS name="QueueComparison.WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/MapQueueComparison.WebInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:QueueComparison.Web:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapQueueComparison.Web:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/DataConnectionString" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:QueueInteralExponent" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/QueueInteralExponent" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:QueueMinInterval" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/QueueMinInterval" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_BufferQuota" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_BufferQuota" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_ConnectionLimit" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_ConnectionLimit" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_DiagnosticsQuota" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_DiagnosticsQuota" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_DisableDiagnostics" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_DisableDiagnostics" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_EnableServiceBusQueue" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_EnableServiceBusQueue" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_EnableStorageQueue" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_EnableStorageQueue" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_EnableWindowsLogs" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_EnableWindowsLogs" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_LogLevel" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_LogLevel" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_LogTransferPeriod" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_LogTransferPeriod" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_PerfSampleRate" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_PerfSampleRate" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_PerfTransferPeriod" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_PerfTransferPeriod" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:REC_SBReceiveTimeout" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/REC_SBReceiveTimeout" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SendGridHost" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SendGridHost" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SendGridPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SendGridPassword" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SendGridPort" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SendGridPort" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SendGridUser" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SendGridUser" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:ServiceBusIssuer" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/ServiceBusIssuer" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:ServiceBusKey" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/ServiceBusKey" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:ServiceBusNamespace" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/ServiceBusNamespace" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:ServiceBusQueue" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/ServiceBusQueue" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SmtpAdminAddress" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SmtpAdminAddress" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SmtpFromAddress" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SmtpFromAddress" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SmtpOutAddress" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SmtpOutAddress" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SmtpOutPort" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SmtpOutPort" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:SmtpService" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/SmtpService" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:StorageQueueConnection" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/StorageQueueConnection" />
          </setting>
        </map>
        <map name="MapQueueComparison.Web:StorageQueueName" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/StorageQueueName" />
          </setting>
        </map>
        <map name="MapQueueComparison.WebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.WebInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="QueueComparison.Web" generation="1" functional="0" release="0" software="C:\Snapboard\Demos\AzureQueueComparisonExample\csx\Debug\roles\QueueComparison.Web" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/SW:QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="QueueInteralExponent" defaultValue="" />
              <aCS name="QueueMinInterval" defaultValue="" />
              <aCS name="REC_BufferQuota" defaultValue="" />
              <aCS name="REC_ConnectionLimit" defaultValue="" />
              <aCS name="REC_DiagnosticsQuota" defaultValue="" />
              <aCS name="REC_DisableDiagnostics" defaultValue="" />
              <aCS name="REC_EnableServiceBusQueue" defaultValue="" />
              <aCS name="REC_EnableStorageQueue" defaultValue="" />
              <aCS name="REC_EnableWindowsLogs" defaultValue="" />
              <aCS name="REC_LogLevel" defaultValue="" />
              <aCS name="REC_LogTransferPeriod" defaultValue="" />
              <aCS name="REC_PerfSampleRate" defaultValue="" />
              <aCS name="REC_PerfTransferPeriod" defaultValue="" />
              <aCS name="REC_SBReceiveTimeout" defaultValue="" />
              <aCS name="SendGridHost" defaultValue="" />
              <aCS name="SendGridPassword" defaultValue="" />
              <aCS name="SendGridPort" defaultValue="" />
              <aCS name="SendGridUser" defaultValue="" />
              <aCS name="ServiceBusIssuer" defaultValue="" />
              <aCS name="ServiceBusKey" defaultValue="" />
              <aCS name="ServiceBusNamespace" defaultValue="" />
              <aCS name="ServiceBusQueue" defaultValue="" />
              <aCS name="SmtpAdminAddress" defaultValue="" />
              <aCS name="SmtpFromAddress" defaultValue="" />
              <aCS name="SmtpOutAddress" defaultValue="" />
              <aCS name="SmtpOutPort" defaultValue="" />
              <aCS name="SmtpService" defaultValue="" />
              <aCS name="StorageQueueConnection" defaultValue="" />
              <aCS name="StorageQueueName" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;QueueComparison.Web&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;QueueComparison.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[8192,8192,8192]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.WebInstances" />
            <sCSPolicyFaultDomainMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.WebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="QueueComparison.WebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="QueueComparison.WebInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="6f313678-cbc6-4d1d-904e-641fb368c237" ref="Microsoft.RedDog.Contract\ServiceContract\AzureQueueComparisonExampleContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="ed63ddb6-1bfa-4d8b-a4fe-c8e530110673" ref="Microsoft.RedDog.Contract\Interface\QueueComparison.Web:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="4d9b87fc-c4ee-4a0f-94eb-e482a7c6d4b9" ref="Microsoft.RedDog.Contract\Interface\QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/AzureQueueComparisonExample/AzureQueueComparisonExampleGroup/QueueComparison.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>
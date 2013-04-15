<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="MultipleSitesOneRole" generation="1" functional="0" release="0" Id="a310667d-5352-4c1d-883f-b52a1f74730a" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MultipleSitesOneRoleGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="ABCSite:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MultipleSitesOneRole/MultipleSitesOneRoleGroup/LB:ABCSite:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="ABCSiteInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MultipleSitesOneRole/MultipleSitesOneRoleGroup/MapABCSiteInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:ABCSite:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MultipleSitesOneRole/MultipleSitesOneRoleGroup/ABCSite/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapABCSiteInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MultipleSitesOneRole/MultipleSitesOneRoleGroup/ABCSiteInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="ABCSite" generation="1" functional="0" release="0" software="C:\Samples\Azure\WindowsAzure\MultipleSitesOneRole\MultipleSitesOneRole\csx\Release\roles\ABCSite" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="768" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ABCSite&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;ABCSite&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MultipleSitesOneRole/MultipleSitesOneRoleGroup/ABCSiteInstances" />
            <sCSPolicyFaultDomainMoniker name="/MultipleSitesOneRole/MultipleSitesOneRoleGroup/ABCSiteFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="ABCSiteFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="ABCSiteInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="41f50779-9a1f-46d5-8708-f0c9d069b708" ref="Microsoft.RedDog.Contract\ServiceContract\MultipleSitesOneRoleContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="497de650-0956-49e0-8abe-b0d716b876e8" ref="Microsoft.RedDog.Contract\Interface\ABCSite:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/MultipleSitesOneRole/MultipleSitesOneRoleGroup/ABCSite:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>
$args >> output.txt
$storagedir = $pwd
$webclient = New-Object System.Net.WebClient
$url = "http://s1.stackify.com/Account/AgentDownload"
$file = "$storagedir\StackifyInstall.exe"
$webclient.DownloadFile($url,$file)
& $file /s /v"ACTIVATIONKEY=$args /qn /l*v .\Log.txt"
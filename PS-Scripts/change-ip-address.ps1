# if gateway changed, you will end with 2 gateways per ip address and generate errors later on. This script needs more work
$gateway = "192.168.1.1"
$interface = "Ethernet"
$ip = "192.168.1.149"
$dns = "8.8.8.8, 8.8.4.4"

Write-Host "Before"
Get-NetIPConfiguration -InterfaceAlias $interface

Write-Host "Update started"
Remove-NetIPAddress –InterfaceAlias $interface -IPAddress $ip
New-NetIPAddress –InterfaceAlias $interface –AddressFamily IPv4 -IPAddress $ip –PrefixLength 24 -DefaultGateway $gateway
Set-DnsClientServerAddress -InterfaceAlias $interface -ServerAddresses $dns
Write-Host "Update completed"

Write-Host "After"
Get-NetIPConfiguration -InterfaceAlias $interface

# restrict rdp access by ip address with windows firewall
# https://securitytutorials.co.uk/restrict-rdp-access-by-ip-address-with-windows-firewall/
# Prerequiste for this script: create windows firewall inboud rule named "CUSTOM RDP BLOCK" to block more than 1 remote addresses.
# Past number of hours is how meany hours back you want to search in the security log 
$Past_n_Hours = [DateTime]::Now.AddHours(-1)

# Collect Failed login events (4625) from the security log 
$badRDPlogons = Get-EventLog -LogName 'Security' -after $Past_n_Hours -InstanceId 4625 | ?{$_.Message -match 'logon type:\s+(3)\s'} | Select-Object @{n='IpAddress';e={$_.ReplacementStrings[-2]} }

# Pull out the Ip Addresses of the failed logins
$getip = $badRDPlogons | group-object -property IpAddress | where {$_.Count -gt 6} | Select -property Name

# Creates Log
$log = "C:\FailedLogins\rdp_blocked_ip.txt"

#Takes the current IPs already in the block list
$current_ips = @()
$current_ips = (Get-NetFirewallRule -DisplayName "CUSTOM RDP BLOCK" | Get-NetFirewallAddressFilter ).RemoteAddress

#Takes each IP captured and adds it to log
foreach ($ip in $getip)
{
    $date = (Get-Date).ToString()
    # writing the IP blocking event to the log file
    $date + ' ' + $ip.name + ' The IP address has been blocked due to ' + ($badRDPlogons | where {$_.IpAddress -eq $ip.name}).count + ' attempts for 1 hour'>> $log 

	$current_ips += $ip.name
}

#Adds current ips to the CUSTOM RDP BLOCK rule
Set-NetFirewallRule -DisplayName "CUSTOM RDP BLOCK" -RemoteAddress $current_ips

# right click security event audit failure and create task to run this ps script. program "powershell.exe" and argument -ExecutionPolicy Bypass -FILE "C:\FailedLogins\FailedLogins.ps1" and open properties after creation. Allow running if user logged in or not and do not store password

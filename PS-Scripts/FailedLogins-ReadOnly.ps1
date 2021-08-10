# Past number of hours is how meany hours back you want to search in the security log 
$Past_n_Hours = [DateTime]::Now.AddHours(-1)

# Collect Failed login events (4625) from the security log 
$badRDPlogons = Get-EventLog -LogName 'Security' -after $Past_n_Hours -InstanceId 4625 | ?{$_.Message -match 'logon type:\s+(3)\s'} | Select-Object @{n='IpAddress';e={$_.ReplacementStrings[-2]} }

# Pull out the Ip Addresses of the failed logins
$getip = $badRDPlogons | group-object -property IpAddress | where {$_.Count -gt 6} | Select -property Name

#Takes each IP captured and write it
$from = $Past_n_Hours.ToString()
Write-Output "from $from to $date"

foreach ($ip in $getip)
{
    $date = (Get-Date).ToString()
    $message = $ip.name + ' The IP address failed to login ' + ($badRDPlogons | where {$_.IpAddress -eq $ip.name}).count + ' times'

    Write-Output $message
}
$deviceName = 'Qualcomm Atheros AR3012 Bluetooth 4.0'

$device = Get-PnpDevice -PresentOnly -FriendlyName $deviceName
Write-Host "disable $deviceName"
Disable-PnpDevice -InstanceId $device.InstanceId -Confirm:$false
Write-Host "sleep for 10 seconds"
Start-Sleep -Seconds 10
Write-Host "enable $deviceName"
Enable-PnpDevice -InstanceId $device.InstanceId -Confirm:$false

Get-PnpDevice -PresentOnly -FriendlyName $deviceName | Select-Object Status, PNPClass, Caption, DeviceID

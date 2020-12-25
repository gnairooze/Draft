$deviceName = 'Qualcomm Atheros AR3012 Bluetooth 4.0'

Get-PnpDevice -PresentOnly -FriendlyName $deviceName | Disable-PnpDevice
Get-PnpDevice -FriendlyName $deviceName | Enable-PnpDevice
Get-PnpDevice -PresentOnly -FriendlyName $deviceName | Select-Object Status, PNPClass, Caption, DeviceID

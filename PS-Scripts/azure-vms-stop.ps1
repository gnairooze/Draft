# this script run in powershell 7.1
# prerequisites: Install Azure PowerShell https://docs.microsoft.com/en-us/powershell/azure/install-az-ps
# login using Connect-AzAccount
# use Get-AzSubscription to read your available subscriptions

# input start
$subscriptionId = "subscription Id"
$vms = @(
    [PSCustomObject]@{Name = "vm name 1"; ResourceGroupName = "resource group name 1"}
    [PSCustomObject]@{Name = "vm name 2"; ResourceGroupName = "resource group name 1"}
)
# input end

$currentSubscriptionId = (Get-AzContext | Select Subscription).Subscription

if ($currentSubscriptionId -like $subscriptionId)
{
	Write-Host "use current subscription $currentSubscriptionId"
}
else
{
	Write-Host "Change subscription from $currentSubscriptionId to $subscriptionId"
	
	Set-AzContext -SubscriptionId $subscriptionId
}

Write-Host "before"
Get-AzVM -Status | Select-Object Name, ResourceGroupName, Location, PowerState | Format-Table -AutoSize -Wrap

foreach ($vm in $vms) {
    Write-Host $vm.Name

    Stop-AzVM -ResourceGroupName $vm.ResourceGroupName -Name $vm.Name
}

Write-Host "after"
Get-AzVM -Status | Select-Object Name, ResourceGroupName, Location, PowerState | Format-Table -AutoSize -Wrap

# this script run in powershell 7.1
# prerequisites: Install Azure PowerShell https://docs.microsoft.com/en-us/powershell/azure/install-az-ps
# login using Connect-AzAccount
# use Get-AzSubscription to read your available subscriptions

$subscriptionId = "subscription Id"
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

Get-AzVM -Status | Select-Object Name, ResourceGroupName, Location, PowerState | Format-Table -AutoSize -Wrap

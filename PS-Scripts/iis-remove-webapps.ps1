# web app structure
# site-a: uses pool1
# |-- app3: uses pool1
# |-- site-a-sub1
#     |-- app4: uses pool1
# site-b: uses pool2

# input start
$appPoolNames = @("pool1", "pool2")
$sites = @("site-a", "site-b")
$virtualDirectories = @(
    [PSCustomObject]@{SiteName = "site-a"; VirtualDirectoryName = "site-a-sub1"; Application= "\"; FullName = "IIS:\Sites\site-a\site-a-sub1"}
)
$webApps = @(
    [PSCustomObject]@{SiteName = "site-a"; AppName = "app3";}
    [PSCustomObject]@{SiteName = "site-a\site-a-sub1"; AppName = "app4"; }
)
# input end

Import-Module -Name WebAdministration

# remove web apps
Write-Host "removing web apps"

foreach ($item in $webApps){
    Write-Host $item.AppName

    Remove-WebApplication -Name $item.AppName -Site $item.SiteName
}

# remove virtual directories
Write-Host "removing virtual directories"

foreach ($item in $virtualDirectories) {
    Write-Host $item.VirtualDirectoryName

    Remove-Item $item.FullName -Recurse
}

# remove sites
Write-Host "removing sites"

foreach ($item in $sites){
    Write-Host $item.SiteName

    Remove-Website -Name $item
}

# remove app pools
Write-Host "removing application pools"

foreach ($item in $appPoolNames) {
    Write-Host $item

    Remove-WebAppPool -Name $item
}

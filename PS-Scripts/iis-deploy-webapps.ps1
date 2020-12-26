# example folder structure of c:\temp\web
# site-a
# |-- app3
# |-- site-a-sub1
#     |-- app4
# site-b

# set input variables
$rootPath = "c:\temp\Web"

$appPoolNames = @("pool1", "pool2")
$sites = @(
    [PSCustomObject]@{SiteName = "site-a"; PhysicalPath = "$rootPath\site-a"; HostHeader="site-a.demo.test"; Port = 443; SSL = $true; ApplicationPool = "pool1"}
    [PSCustomObject]@{SiteName = "site-b"; PhysicalPath = "$rootPath\site-b"; HostHeader = "site-b.demo.test"; Port = 443; SSL = $true; ApplicationPool = "pool2"}
)
$virtualDirectories = @(
    [PSCustomObject]@{SiteName = "site-a"; VirtualDirectoryName = "site-a-sub1"; PhysicalPath = "$rootPath\site-a\site-a-sub1"}
)

$webApps = @(
    [PSCustomObject]@{SiteName = "site-a"; AppName = "app3"; PhysicalPath = "$rootPath\site-a\app3"; ApplicationPool = "pool1"}
    [PSCustomObject]@{SiteName = "site-a\site-a-sub1"; AppName = "app4"; PhysicalPath = "$rootPath\site-a\site-a-sub1\app4"; ApplicationPool = "pool1"}
)
# end of input

Import-Module -Name WebAdministration

# create application pools
Write-Host "creating app pools"

foreach ($item in $appPoolNames) {
    Write-Host $item

    New-WebAppPool -Name $item
}

# create websites
Write-Host "creating sites"

foreach ($item in $sites){
    Write-Host $item.SiteName

    if ($item.SSL)
    {
        New-Website -Name $item.SiteName -PhysicalPath $item.PhysicalPath -Port $item.Port -Ssl -HostHeader $item.HostHeader -ApplicationPool $item.ApplicationPool
    }
    else 
    {
        New-Website -Name $item.SiteName -PhysicalPath $item.PhysicalPath -Port $item.Port -HostHeader $item.HostHeader -ApplicationPool $item.ApplicationPool
    }

    Get-Website -Name $item.SiteName | Start-Website
}

# create virtual directories
Write-Host "creating virtual directories"

foreach ($item in $virtualDirectories) {
    Write-Host $item.VirtualDirectoryName

    New-WebVirtualDirectory -Site $item.SiteName -Name $item.VirtualDirectoryName -PhysicalPath $item.PhysicalPath
}

# create web apps
Write-Host "creating web apps"

foreach ($item in $webApps){
    Write-Host $item.AppName

    New-WebApplication -Name $item.AppName -Site $item.SiteName -PhysicalPath $item.PhysicalPath -ApplicationPool $item.ApplicationPool
}

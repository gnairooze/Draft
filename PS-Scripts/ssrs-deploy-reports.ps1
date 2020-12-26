# this scripts is using microsoft reporting services tools https://github.com/microsoft/ReportingServicesTools

$reportServerUri = "http://localhost/ReportServer"
$reportRoot = "c:\temp\report"

$folders = @(
    [PSCustomObject]@{Path = "/"; Name = "Sector"}
    [PSCustomObject]@{Path = "/Sector"; Name = "Department"}
    [PSCustomObject]@{Path = "/Sector/Department"; Name = "Sub1"}
    [PSCustomObject]@{Path = "/Sector/Department/Sub1"; Name = "Data-Sources"}
    [PSCustomObject]@{Path = "/Sector/Department/Sub1"; Name = "Reports"}
)

$reports = @(
    [PSCustomObject]@{Path = "$reportRoot\sub1\report1.rdl"; Name = "report1 name"; Destination = "/Sector/Department/Sub1/Reports"}
    [PSCustomObject]@{Path = "$reportRoot\sub1\report2.rdl"; Name = "report2 name"; Destination = "/Sector/Department/Sub1/Reports"}
)

# create folders
foreach ($folder in $folders) {
    Write-Host $folder

    New-RsFolder -ReportServerUri $reportServerUri -Path $folder.Path -Name $folder.Name -Verbose
}

# create reports
foreach ($report in $reports) {
    Write-Host $report.Name

    Write-RsCatalogItem -ReportServerUri $reportServerUri -Path $report.Path -Destination $report.Destination -Name $report.Name
}

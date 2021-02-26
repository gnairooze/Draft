
$connectionString = "Data Source=.;Initial Catalog=Draft;Integrated Security=True;"
$rootPath = "c:\temp\10\scripts"

$files = @(
"$rootPath\1.sql",
"$rootPath\2.sql"
)

# Import-Module sqlps <#for old power shell versions not core#>
# for powershell core as 7.0, just install SQLServer module from https://www.powershellgallery.com/packages/SqlServer

foreach ($file in $files) {
    Write-Host $file

    Invoke-Sqlcmd -InputFile $file -ConnectionString $connectionString
}


$connectionString = "Data Source=.;Initial Catalog=Draft;UID=sqluser;PWD=87654321"
$rootPath = "c:\temp\10\scripts"

$files = @(
"$rootPath\1.sql",
"$rootPath\2.sql"
)

Import-Module sqlps

foreach ($file in $files) {
    Write-Host $file

    Invoke-Sqlcmd -InputFile $file -ConnectionString $connectionString
}

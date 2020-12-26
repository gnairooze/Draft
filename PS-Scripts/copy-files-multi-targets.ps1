# start input
$sourcePath = "c:\temp\source"
$targetPaths = @("c:\temp\target1", "c:\temp\target2")
# end input

Write-Host "copy started"

foreach ($targetPath in $targetPaths) {
    Write-Host "copy to $targetPath started"

    Copy-Item -Path $sourcePath -Destination $targetPath -Recurse -Force

    Write-Host "copy to $targetPath completed"
}

Write-Host "copy completed"

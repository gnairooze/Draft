# start input
$variables = "C:\Files\vars.key" # file contains key value pairs in the format key|value
$sourcePath = "C:\Files" # path whare files that includes the variables will be replaced with its values.
$filter = "*.txt" # file extension that will filter the files in the source path.
# end input

# read key vlaue pairs from environment file
$data = [System.Collections.ArrayList]@()
Get-Content $variables | Foreach-Object{
    $keyValue = $_.Split("|")
    $var =  [PSCustomObject]@{Name = $keyValue[0]; Value = $keyValue[1]}
    $data.Add($var)
}

# add files to array
$files = [System.Collections.ArrayList]@()
(Get-ChildItem -Path $sourcePath -Include $filter -Recurse).FullName | Foreach-Object{
    $files.Add($_)
}

# loop over config files
foreach ($file in $files){
    Write-Host $file

    # loop over key value pairs from variables file to replace them if exist in the file
    foreach ($item in $data) {
        $content = Get-Content -Path $file
        $newContent = $content -replace $item.Name, $item.Value
        $newContent | Set-Content -Path $file
    }
}

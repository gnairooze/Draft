
$Path = "C:\temp\1\IP-Notify"
$Text = "Serilog"
$PathArray = [System.Collections.ArrayList]@()
$Results = "C:\temp\test.txt"

$Files = Get-ChildItem $Path -Filter "*.*" | Where-Object { $_.Attributes -ne "Directory"}

Get-ChildItem $Path -Filter "*.*" | Where-Object { $_.Attributes -ne "Directory"} |
    ForEach-Object {
        If (Get-Content $_.FullName | Select-String -Pattern $Text) {
            $PathArray += $_.FullName
    }
}


Write-Host $PathArray
Set-Content -Path $Results $PathArray


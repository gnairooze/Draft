# list all repos
# read Personal Access Token from user environment variable AZURE_DEVOPS_PAT
$connectionToken = (Get-ChildItem -Path Env:\AZURE_DEVOPS_PAT | Select Value).Value
$project = "project-name"
$org = "organization-name"
$apiVersion = "5.0"
$base64AuthInfo= [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes(":$($connectionToken)"))
$ProjectUrl = "https://dev.azure.com/$org/$project/_apis/git/repositories?api-version=$apiVersion" 
$Repos = (Invoke-RestMethod -Uri $ProjectUrl -Method Get -UseDefaultCredential -Headers @{Authorization=("Basic {0}" -f $base64AuthInfo)})

ForEach($repo in $Repos)
{
    Write-Output $repo.value.Name 
}

param ([string]$migrationName = $( Read-Host "Enter migration name" ))

Write-Host 'Migrating app DB schema...'
dotnet ef migrations add $migrationName --project ..\Web\QLector.DAL.EF\QLector.DAL.EF.csproj --context AppDbContext --verbose --prefix-output
Write-Host '*****************'
Write-Host 'DONE'
Write-Host '*****************'

Write-Host 'Updating app DB schema...' 
dotnet ef database update --project ..\Web\QLector.DAL.EF\QLector.DAL.EF.csproj --context QLector.DAL.EF.AppDbContext --verbose --prefix-output
Write-Host '*****************'
Write-Host 'DONE'
Write-Host '*****************'
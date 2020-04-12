# TODO:
# Some error handling
# Pre-requisites check

$CoverageDirAbsPath = Join-Path -Path (Resolve-path .) -ChildPath '.\coverage'
Write-Host 'Coverage path' $CoverageDirAbsPath

if (Test-Path $CoverageDirAbsPath) {
    Write-Host 'Removing old coverage folder...'
    Remove-Item -Recurse -Force $CoverageDirAbsPath
}

New-Item -ItemType Directory -Force $CoverageDirAbsPath

dotnet test ..\tests\QLector.Application.Tests\QLector.Application.Tests.csproj --results-directory $CoverageDirAbsPath --collect:"Code Coverage"

$reportPath = Get-Childitem -Path $CoverageDirAbsPath -Include "*.coverage" -Recurse | Select-Object -First 1
Write-Host 'Found report' $reportPath

Write-Host 'Converting .coverage to xml format'
$CodeCoverageExePath = Join-Path -Path $ENV:UserProfile -ChildPath '.nuget\packages\microsoft.codecoverage\16.5.0\build\netstandard1.0\CodeCoverage\CodeCoverage.exe'
$CoverXmlPath = Join-Path -Path $CoverageDirAbsPath -ChildPath 'coverage.xml'
Write-Host 'Using CodeCoverage.exe' $CodeCoverageExePath
Write-Host 'CoverXmlPath' $CoverXmlPath
& $CodeCoverageExePath analyze /output:$CoverXmlPath $reportPath

Write-Host 'Generating report using ReportGenerator.dll'
$ReportGeneratorDllPath = Join-Path -Path $ENV:UserProfile -ChildPath '.nuget\packages\reportgenerator\4.5.5\tools\netcoreapp3.0\ReportGenerator.exe'

Write-Host 'Using ReportGenerator.dll' $ReportGeneratorDllPath
& $ReportGeneratorDllPath -reports:$CoverXmlPath -targetdir:$CoverageDirAbsPath

Write-Host 'Your report is ready in coverage directory!' $CoverageDirAbsPath
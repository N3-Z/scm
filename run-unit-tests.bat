@echo off

dotnet test "%~dp0\src\ScmBackup.Tests\ScmBackup.Tests.csproj" -c Release
pause
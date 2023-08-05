:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Install TerrariaImageStitcher Via MSBuild                      ::
::Gihub https://github.com/RussDev7/TerrariaImageStitcher        ::
::Devoloped, Maintained, And Sponsored By D.RUSS#2430            ::
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
@ECHO OFF

Rem | Set Params
Set "VersionPrefix=1.3.0.0"
Set "filename=TerrariaImageStitcher-%VersionPrefix%"

Rem | Install SLN Under x64 Profile
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe ".\src\TerrariaImageStitcher.sln" /p:Configuration=Release /p:Platform=x64"

Rem | Delete Paths & Create Paths
rmdir /s /q ".\release"
mkdir ".\release"

Rem | Copy Over Items
xcopy /E /Y ".\src\TerrariaImageStitcher\bin\x64\Release" ".\release\%filename%\"

Rem | Clean Up Files
del /f /q /s ".\release\*.pdb"
del /f /q /s ".\release\*.config"

Rem | Delete & Create ZIP Release
if exist ".\%filename%.zip" (del /f ".\%filename%.zip")
powershell.exe -nologo -noprofile -command "Compress-Archive -Path ".\release\*" -DestinationPath ".\%filename%.zip""

Rem | Operation Complete
echo(
pause

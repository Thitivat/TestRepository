@echo off
SET PublishLocation=\\atticsoft-srv01\Sites\BND.Services.Funds.WebService\
SET BinLocation=\\atticsoft-srv01\Sites\BND.Services.Funds.WebService\bin\

REM SET PublishLocation = atticsoft-srv01 --> D:\Sites\BND.Services.Funds.WebService\
REM SET BinLocation = atticsoft-srv01 --> D:\Sites\BND.Services.Funds.WebService\bin\

SET /P ANSWER=Proceed with publishing (y/n)? 

if /i {%ANSWER%}=={y} (goto :yes)
if /i {%ANSWER%}=={Y} (goto :yes) 

goto :no 

:yes
echo Removing "%BinLocation%" folder

rd /s /q %BinLocation%

echo Publishing to "%PublishLocation%"

xcopy BND.Services.Funds.WebService\App_Data %PublishLocation%App_Data\ /y /s
xcopy BND.Services.Funds.WebService\bin %PublishLocation%bin\ /y /s
xcopy BND.Services.Funds.WebService\Config\*.config %PublishLocation%Config\ /y
xcopy BND.Services.Funds.WebService\Global.asax %PublishLocation% /y
xcopy BND.Services.Funds.WebService\web.config %PublishLocation% /y

echo Publishing done, see ya! 
pause
exit /b 0 

:no
echo Well, maybe another time then, bye!
pause
exit /b 1 
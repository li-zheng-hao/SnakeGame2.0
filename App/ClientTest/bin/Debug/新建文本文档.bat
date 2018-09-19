@echo off
setlocal enabledelayedexpansion
set "a=0"
:loop
start "" /b /wait "ClientTest.exe"
set /a "a=!a!+1"
echo ´ÎÊý!a!
if %a%==1000 exit
goto loop
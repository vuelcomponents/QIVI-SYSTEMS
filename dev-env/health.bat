@echo off

set lombanditClient=1920
set lombanditServer=1820
set authClient=1810
set authServer=1910


for %%i in (%lombanditClient% %lombanditServer% %authClient% %authServer%) do (
    for /f "tokens=2,3" %%a in ('netstat -aon ^| findstr ":%%i" ^| findstr /i "LISTENING"') do (
        echo Port %%i is in use by process ID %%b on host %%a
    )
)


echo QAURX SYSTEMS status checked on ports 1909 to 1920.
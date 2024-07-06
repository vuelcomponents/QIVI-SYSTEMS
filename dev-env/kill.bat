@echo off
set lombanditClient=1920
set lombanditServer=1820
set authClient=1810
set authServer=1910


for %%i in (%lombanditClient% %lombanditServer% %authClient% %authServer%) do (
   echo Terminating HRTechnique on port %%i
       for /f "tokens=5" %%a in ('netstat -aon ^| findstr ":%%i" ^| findstr /i "LISTENING"') do (
           taskkill /PID %%a /F
           if errorlevel 1 (
               echo Failed to terminate processes on port %%i
           ) else (
               echo Successfully terminated processes on port %%i
           )
       )
)

echo HRTechnique is down
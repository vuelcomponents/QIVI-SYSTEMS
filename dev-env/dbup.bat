@echo off

:: Terminate processes on port 1916
echo Terminating HRTechnique on port 1910
for /f "tokens=5" %%a in ('netstat -aon ^| findstr ":1910" ^| findstr /i "LISTENING"') do (
    taskkill /PID %%a /F
    if errorlevel 1 (
        echo Failed to terminate processes on port 1910
    ) else (
        echo Successfully terminated processes on port 1910
    )
)

@echo off
start wt -w 0 nt -p "authserver" -d "C:\Users\mat\Desktop\HRTechnique\hrtAuthServer" cmd /c "echo y | dotnet ef database drop --context AuthDataContext && dotnet ef migrations remove --context AuthDataContext && dotnet ef migrations add InitMigration --context AuthDataContext && dotnet ef database update --context AuthDataContext && dotnet run &"
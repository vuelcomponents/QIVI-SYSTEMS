@echo off
1.2
start wt -w 0 nt -p "webstormrunning1" -d "C:\Program Files\JetBrains\WebStorm 2024.1.22\bin" ^
     cmd /c "title [HRT] Webstorm running && start .\webstorm64.exe C:\Users\mat\Desktop\HRTechnique\hrtAuthClient && timeout /t 4 >nul && start .\webstorm64.exe C:\Users\mat\Desktop\HRTechnique\HrTechniqueClient && start .\webstorm64.exe C:\Users\mat\Desktop\HRTechnique\LombanditClient"

start wt -w 0 nt -p "riderrunning1" -d "C:\Program Files\JetBrains\JetBrains Rider 2024.1.2\bin" ^
    cmd /c "title [HRT] Rider running && start .\rider64.exe C:\Users\mat\Desktop\HRTechnique\hrtAuthServer\authServer.sln && timeout /t 4 >nul && start .\rider64.exe C:\Users\mat\Desktop\HRTechnique\LombanditServer\LombanditServer.sln  && timeout /t 4 >nul && start .\rider64.exe C:\Users\mat\Desktop\HRTechnique\HrTechniqueServer\HrTechniqueServer.sln"

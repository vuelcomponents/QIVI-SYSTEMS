@echo off
start wt -w 0 nt p "webstormrunning1" -d "C:\Program Files\JetBrains2\WebStorm 2024.1\bin" ^
     cmd /c "title [HRT] Webstorm running && start webstorm64.exe C:\Users\mat\Desktop\HRTechnique\hrtAuthClient &"
timeout /t 4 >nul
start wt -w 0 nt -p "webstormrunning1" -d "C:\Program Files\JetBrains2\WebStorm 2024.1\bin" ^
    cmd /c "title [HRT] Webstorm running && start webstorm64.exe C:\Users\mat\Desktop\HRTechnique\hrtDataClient &"

start wt -w 0 nt -p "riderrunning1" -d "C:\Program Files\JetBrains\JetBrains Rider 2023.3.4\bin"
    cmd /c "title [HRT] Rider running && start rider64.exe C:\Users\mat\Desktop\HRTechnique\hrtAuthServer\authServer.sln &"
timeout /t 4 >nul
start wt -w 0 nt -p "riderrunning1" -d "C:\Program Files\JetBrains\JetBrains Rider 2023.3.4\bin"
    cmd /c "title [HRT] Rider running && start rider64.exe C:\Users\mat\Desktop\HRTechnique\hrtDataServer\hrtDataServer.sln  &"
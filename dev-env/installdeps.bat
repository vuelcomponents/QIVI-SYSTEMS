@echo off
cmd /c "kill.bat"

@echo off
start wt -w 0 nt -p "authclient" -d "C:\Users\mat\Desktop\HRTechnique\hrtAuthClient" cmd /c "title [HRT] Auth Client && npm i &"
@echo off
start wt -w 0 nt -p "dataclient" -d "C:\Users\mat\Desktop\HRTechnique\hrtDataClient" cmd /c "title [HRT] Data Client && npm i &"
@echo off
start wt -w 0 nt -p "dataserver" -d "C:\Users\mat\Desktop\HRTechnique\hrtDataServer" cmd /c "title [HRT] Data Server && dotnet build &"
@echo off
start wt -w 0 nt -p "authserver" -d "C:\Users\mat\Desktop\HRTechnique\hrtAuthServer" cmd /c "title [HRT] Auth Server && dotnet build &"

cmd /c "dev.bat"

echo HRTechnique restarted
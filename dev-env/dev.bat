@echo off
start wt -w 0 nt -p "authclient" -d "C:\Users\mat\Desktop\HRTechnique\hrtAuthClient" cmd /c "title [HRT] Auth Client && npm run host &"
@echo off
start wt -w 0 nt -p "dataclient" -d "C:\Users\mat\Desktop\HRTechnique\LombanditClient" cmd /c "title [HRT] Data Client && npm run host &"
@echo off
start wt -w 0 nt -p "dataserver" -d "C:\Users\mat\Desktop\HRTechnique\LombanditServer" cmd /c "title [HRT] Data Server && dotnet run &"
@echo off
start wt -w 0 nt -p "authserver" -d "C:\Users\mat\Desktop\HRTechnique\hrtAuthServer" cmd /c "title [HRT] Auth Server && dotnet run &"

echo QUARX SYSTEMS is running
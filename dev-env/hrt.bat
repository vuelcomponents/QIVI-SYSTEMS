@echo off
if "%1"=="dev" (
   cmd /c "dev.bat"
)
if "%1"=="kill" (
    cmd /c "kill.bat"
)
if "%1"=="restart" (
    cmd /c "restart.bat"
)
if "%1"=="health" (
    cmd /c "health.bat"
)
if "%1"=="dbup" (
    cmd /c "dbup.bat"
)
if "%1"=="ide" (
    if "%2"=="kill" (
        cmd /c "killide.bat"
    )
    if "%2"=="open" (
        cmd /c "openide.bat"
    )
)
if "%1"=="libup" (
    cmd /c "libup.bat"
)
if "%1"=="installdeps" (
    cmd /c "installdeps.bat"
)
if "%1"=="browse" (
    cmd /c "browse.bat"
)
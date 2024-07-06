@echo off
start firefox -new-tab http://localhost:1810
timeout /t 1 >nul
start firefox -new-tab http://localhost:1910/swagger

timeout /t 1 >nul
start firefox -new-tab http://localhost:1820
timeout /t 1 >nul
start firefox -new-tab http://localhost:1920/swagger

:: Sprawdź zdrowie AuthClient
ping -n 1 localhost -w 1000 | find "TTL" >nul && (
    set health=AuthClient: true/1
) || (
    set health=AuthClient: false/0
)

:: Sprawdź zdrowie AuthServer
ping -n 1 localhost -w 1000 | find "TTL" >nul && (
    set health=%health%, AuthServer: true/1
) || (
    set health=%health%, AuthServer: false/0
)

echo %health%
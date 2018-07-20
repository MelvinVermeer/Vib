# Vib
Deze repository bevat de registratie site van de Carrieredag van Vereniging in Beweging

Azure Resources (bevat ook deploy stap) ![](https://melvinvermeer.visualstudio.com/_apis/public/build/definitions/063fbab4-8dd7-4323-9a94-aa17e644857d/20/badge?branch=master)

Azure WebApp ![](https://melvinvermeer.visualstudio.com/_apis/public/build/definitions/063fbab4-8dd7-4323-9a94-aa17e644857d/21/badge?branch=master) ![](https://melvinvermeer.vsrm.visualstudio.com/_apis/public/Release/badge/063fbab4-8dd7-4323-9a94-aa17e644857d/1/1)

## Environent settings
Deze applicatie maakt gebruik van Google en Facebook authenticatie. Zorg ervoor de volgende appsettings beschikbaar zijn op de omgeving
- Authentication:Facebook:AppId
- Authentication:Facebook:AppSecret
- Authentication:Google:ClientId
- Authentication:Google:ClientSecret

Of als je de applicatie lokaal wilt draaien zorg voor een appsecrets.json die er alsvolgt uitziet:

```
{
  "Authentication": {
    "Facebook": {
      "AppId": "...",
      "AppSecret": "..."
    },
    "Google": {
      "ClientId": "...",
      "ClientSecret": "..."
    }
  }
}
```


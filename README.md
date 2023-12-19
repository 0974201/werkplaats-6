# Kraansimulatie Project

Het doel van dit project is om een interactieve kraansimulatie te creëren. Het zal MQTT gebruiken voor real-time communicatie, 
React.js gebruiken voor de frontend, Python gebruiken voor de backend en MongoDB gebruiken voor het opslaan van data. 

## Kenmerken

* Realistische 3D-simulatie van een kraan.
* Bediening van de kraan via een dashboard (webinterface).
* Real-time statusupdates van de kraan met behulp van MQTT.
* Simulatie van kraan bewegingen en berekeningen maken in Python.

## Vereisten (DoD)

- [ ] Implementatie van de kraan in de backend.
- [ ] (voeg andere vereisten toe die je hebt voltooid of van plan bent om te voltooien)

## Installatie en setup

### Benodigdheden

* Node.js
* React.js
* Vite.js
* Python 3.x
* C Sharp
* HiveMQ (Mqtt broker)
* MongoDB

## Backend 
1. Open een Terminal 
2. Navigeer naar de Backend Map:
    *  Gebruik het commando ```cd pad/naar/jouw/project/backend``` om naar de backend map te gaan.
3. Installeer Python Afhankelijkheden:
    *   Zorg ervoor dat Python geïnstalleerd is op je systeem. Je kunt de installatie verifiëren met ```python --version```.
    *   Installeer de vereiste pakketten met ```pip install -r requirements.txt```. Dit commando leest de requirements.txt file en installeert alle benodigde Python-pakketten.
4. Start de Backend Server:
    *   Start de server met ```python main.py```. Dit zal de backend applicatie draaien die nodig is voor de kraansimulatie.

## Frontend 
1. Open een Terminal
2. Navigeer naar de Frontend Map:
   *  Gebruik het commando ```cd pad/naar/jouw/project/frontend``` om naar de frontend map te gaan.
3. Installeer Node.js Afhankelijkheden:
   *  Zorg ervoor dat Node.js en npm geïnstalleerd zijn op je systeem. Je kunt de installatie verifiëren met ```node --version``` en ```npm --version```.
   *  Installeer de benodigde Node-modules met het commando ```npm install```. Dit zal alle afhankelijkheden installeren die gedefinieerd zijn in je package.json bestand.
4. Start de React-applicatie:
   *  Start de React-frontend met npm start. Dit zal de ontwikkelserver opstarten en de applicatie beschikbaar maken in je webbrowser.

## HiveMQ MQTT Broker 

HiveMQ biedt een eenvoudige en krachtige MQTT Broker die ideaal is voor IoT-toepassingen

1. Ga naar de [HiveMQ](https://www.hivemq.com) en download de nieuwste versie.
2. Pak het gedownloade bestand uit naar een directory naar keuze.
3. Open de terminal, navigeer naar de uitgepakte directory en start de broker met:
```./bin/run.sh```
4. Open een webbrowser en ga naar http://localhost:8080. Je zou de HiveMQ control panel moeten zien.

## MongoDB 

MongoDB is een document-gebaseerde NoSQL-database die gebruikt wordt voor het opslaan van projectgerelateerde data. 

1. Ga naar de [MongoDB](https://www.mongodb.com) en kies de versie die past bij jouw besturingssysteem.
2. Volg de installatie-instructies op de MongoDB website.
3. Afhankelijk van je systeem, start MongoDB met een commando zoals:
```mongod```
4. Maak een verbinding met de standaard MongoDB-poort ```27017``` om te controleren of de database draait.

## Projectstructuur

    st-2324-1-d-wx1-t2-2324-wx1-bear/
    │
    ├── docs/                    
    │   ├── conventions/                   
    │   │   ├── base_positions.md      
    │   │   ├── components.md         
    │   │   ├── metrics.md            
    │   │   ├── mqtt_messages.md      
    │   │   └── ...                  
    │   │
    │   ├── microservices/              
    │   │   ├── broker.md            
    │   │   ├── controller.md
    │   │   ├── dashboard.md
    │   │   ├── database.md
    │   │   └── ...    
    │
    ├── run/                     
    │   │   ├── linux_mac.sh       
    │   │   ├── windows.bat        
    │   │   └── ...                   
    │
    ├── src/                     
    │   ├── CSharp/  
    │   │   ├── Controller.wpf          
    │   │   ├── ControllerXBOX          
    │   │   ├── CraneSim.Core         
    │   │   ├── CraneSim.Infrastructure         
    │   │   ├── CraneSim       
    │   │   ├── MQTTTemplate         
    │   │   ├── CraneSim.sln          
    │   │   └── ...                   
    │   ├── Python/  
    │   │   ├── README.md         
    │   │   ├── boom.py         
    │   │   ├── emergencyButton.py   
    │   │   └── ...  
    │   ├── back_end/  
    │   │   ├──          
    │   │   ├── container.py        
    │   │   ├── db_mqtt_client.py 
    │   │   ├── hoist.py
    │   │   ├── main.py 
    │   │   ├── setup.py 
    │   │   ├── spreader.py 
    │   │   └── ...
    │   ├── front_end/  
    │   │   ├──          
    │   │   └── ...   
    │
    ├── .gitignore  
    ├── README.md                    # Projectdocumentatie
    ├── Requirements.txt             # Packages

## Gebruik

1. Open de webbrowser en navigeer naar http://localhost:3000 om de interactieve dashboard van de kraansimulatie te gebruiken.
2. Gebruik de bedieningselementen op de webpagina om de kraan in de simulatie te bedienen. Dit omvat bewegingen zoals omhoog, omlaag, links, rechts, en het oppakken of neerzetten van lading.
3. De huidige status en bewegingen van de kraan worden real-time gevisualiseerd in de 3D-weergave op het dashboard.

## Contributie

Andy, Tristan, Zasha, Martijn, Yassine, Miro, Suman, Quylian

*Movement trolley readme*

*TrolleyService Klasse*
De TrolleyService klasse is een onderdeel van een service laag die communiceert met de Trolley entiteit. Het biedt methoden om de beweging van een trolley te berekenen en te beheren. Hier is een korte beschrijving van de methoden:


*CalculateConstantAccelaration(Trolley entity):* 

Deze methode berekent de constante versnelling van de trolley op basis van de huidige snelheid, maximale snelheid en versnellings-/vertragingsduur.

*CalculateCurrentSpeed(Trolley entity):*

Deze methode berekent de huidige snelheid van de trolley op basis van de verstreken tijd en de versnelling, vertraging en maximale en minimale snelheidswaarden van de trolley.

*CalculateHorizontaleNegatiefMovement(Trolley entity) en CalculateHorizontalePositiefMovement(Trolley entity):*

Deze methoden berekenen de nieuwe horizontale positie van de trolley door respectievelijk de afgelegde afstand af te trekken of op te tellen bij de huidige positie. De afgelegde afstand wordt berekend op basis van de huidige snelheid en de verstreken tijd.

*ResetStopWatch(), StartStopwatch(), StopStopwatch():*

Deze methoden regelen de stopwatch die de verstreken tijd voor de beweging van de trolley meet.

*ReturnStopwatchvalue():*

Deze methode geeft de waarde van de stopwatch in seconden terug.

Deze klasse gebruikt de Stopwatch klasse om de verstreken tijd te meten, wat cruciaal is voor het berekenen van de snelheid en positie van de trolley. Het async Task retourtype voor alle methoden geeft aan dat deze methoden asynchroon zijn, wat betekent dat ze kunnen draaien zonder de uitvoering van andere code te blokkeren. Dit is bijzonder nuttig in real-time applicaties waar je niet wilt dat de UI bevriest terwijl de beweging van de trolley wordt berekend.

Let op dat de daadwerkelijke beweging van de trolley en de interactie met de UI elders in de applicatie wordt afgehandeld, waarschijnlijk in een controller of een view model. Deze service klasse is strikt voor het uitvoeren van berekeningen met betrekking tot de beweging van de trolley.

Vergeet niet om altijd StartStopwatch() te bellen voordat je de berekeningsmethoden aanroept, en StopStopwatch() wanneer de trolley stopt met bewegen. Je kunt ResetStopWatch() gebruiken om de stopwatch te resetten voordat je een nieuwe beweging start.

De Trolley entiteit moet eigenschappen bevatten zoals Speed, Acceleration, Deceleration, MaximumSpeedValue, MinimumSpeedValue, PositionX, MaxPositionX, MinPositionX, en AccelAndDecelarationTime. Deze eigenschappen worden gebruikt in de berekeningen.

Deze klasse implementeert de ITrolleyService interface, dus het kan gemakkelijk worden vervangen door een andere implementatie indien nodig, wat een goede praktijk is voor het behoud van een flexibele en testbare codebase.

Raadpleeg de daadwerkelijke code voor meer details en zorg ervoor dat je de logica achter elke berekening begrijpt voordat je deze service in je applicatie gebruikt.

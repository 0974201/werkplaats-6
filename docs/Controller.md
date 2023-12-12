## Controller

Er werden een aantal knoppen ingesteld met hun acties die ze zouden moeten doen. Dit in een wpf applicatie.
Om te zien of dit werkt pas ik een Label aan die Toont welke knop er wordt ingedrukt of losgelaten.
Er is een noodstop. Deze weerhoud alle buttons om een signaal door te geven. Toch tot dat de vergrendeling weer uit staat.

 - W = Trolley forward!
 - S = Trolley backwards!
 - A = Gantry left!
 - D = Gantry right!
 - Up = Hoist up!
 - Down = Hoist down! 
 - Q = Boom Up!
 - E = Boom Down!
 - O = Noodstop has been pressed!!!! Press P to reset
 - P = Undid the Noodstop!

 De topics:
 - vooruit Trolley => crane/components/trolley/1
 - achteruit Trolley => crane/components/trolley/2
 - stop Trolley => crane/components/trolley/0

 - vooruit Gantry => crane/components/gantry/1
 - achteruit Gantry => crane/components/gantry/2
 - stop Gantry => crane/components/gantry/0

 - vooruit Hoist => crane/components/hoist/1
 - achteruit Hoist => crane/components/hoist/2
 - stop Hoist => crane/components/hoist/0

 - vooruit Boom => crane/components/boom/1
 - achteruit Boom => crane/components/boom/2
 - stop Boom => crane/components/boom/0

 - noodstop => meta/emergency_button
 (luistert of de bool isPressed tru of false is)
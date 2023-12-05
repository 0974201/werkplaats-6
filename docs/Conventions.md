# Conventions
Hard guidelines for maintaining/operating/developing this project.


## MQTT messages
### Types of messages
 - Command Emergency Button Message
   - = Command to stop entire system
   - topic = "meta/emergency_button"
   - sender = controller
 - Component State Message
   - = Current state from this component
   - topic = "crane/components/_component_/state"
   - sender = component
 - Command Message
   - = Command to specific component
   - topic = "crane/components/_component_/command"
   - sender = controller
 - Composed Sensor Data Message
   - = Current state of all components of crane
   - topic = "crane/composed_states/composed_state"
   - sender = crane
 - Error Message
   - = Error messages
   - topic = "meta/errors"
   - sender = _any microservice_


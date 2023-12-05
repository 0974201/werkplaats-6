# Conventions
Hard guidelines for maintaining/operating/developing this project.


## MQTT messages
= each message will have a topic property, this is equal to its MQTT topic 
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

### Templates
#### All Messages
```JSON
{
   "meta": {
      "topic": str,
  },
   "msg": {
   }
}
```
#### Command Messages
```JSON
{
   "meta": {
      "topic": str,
  },
   "msg": {
      "target": str,
      "command": bool
   }
}
```
#### State Messages
- All State Messages
   ```JSON
   {
      "meta": {
         "topic": str,
         "active": bool
     },
      "msg": {
         "target": str,
         "command": bool
      }
   }
   ```
- Container
   ```JSON
   {
      "meta": {
         "topic": "crane/components/container/state",
         "id": int,
         "active": bool,
     },
      "msg": {
         "connected": bool,
         "positions": {
            "x": float,
            "y": float,
            "z": float
        },
        "speed": {
          "speed": float
        }
      }
   }
   ```
- Hoist
   ```JSON
   {
      "meta": {
         "topic": "crane/components/hoist/state",
         "active": bool
     },
      "msg": {
         "connected": bool
         "positions": {
            "x": float,
            "y": float,
            "z": float
        },
        "speed": {
          "activeAcceleration": bool
          "acceleration": float,
          "speed": float
        }
      }
   }
   ```
  - Hoist
   ```JSON
   {
      "meta": {
         "topic": "crane/components/hoist/state",
         "active": bool
     },
      "msg": {
         "connected": bool
         "positions": {
            "x": float,
            "y": float,
            "z": float
        },
        "speed": {
          "activeAcceleration": bool
          "acceleration": float,
          "speed": float
        }
      }
   }
   ```
#### Error Messages
```JSON
{
   "meta": {
      "topic": "meta/errors"
  },
   "msg": {
      "type": str,
      "microservice": str
      "error": str
   }
}
```
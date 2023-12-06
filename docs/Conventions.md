# Conventions
Hard guidelines for maintaining/operating/developing this project.

## Metrics
- Acceleration:
- Speed: 
- Dimensions: 


## MQTT messages
= each message will have a topic property, this is equal to its MQTT topic 
### Topics
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
 - Crane State Message
   - = Current state of all components of the crane
   - topic = "crane/state"
   - sender = crane
 - Error Message
   - = Error messages
   - topic = "meta/errors"
   - sender = _any microservice_

### Templates
#### All Messages
```JSON
{
   "meta":{
      "topic":"str"
   },
   "msg":{
      
   }
}
```
#### Command Messages
```JSON
{
   "meta":{
      "topic":"str"
   },
   "msg":{
      "target":"str",
      "command":"bool"
   }
}
```
#### State Messages
- All State Messages
   ```JSON
    {
       "meta":{
          "topic":"str",
          "isActive":"bool",
          "component":"str"
       },
       "msg":{
          "command":"bool"
       }
    }
   ```
- Container
    ```JSON
    {
       "meta":{
          "topic":"crane/components/container/state",
          "id":"int",
          "isActive":"bool",
          "component":"container"
       },
       "msg":{
          "isConnected":"bool",
          "absolutePosition":{
             "x":"float",
             "y":"float",
             "z":"float"
          },
          "speed":{
             "speed":{
                "x":"float",
                "y":"float",
                "z":"float"
             }
          }
       }
    }
   ```
- Hoist
   ```JSON
    {
       "meta":{
          "topic":"crane/components/hoist/state",
          "isActive":"bool",
          "component":"hoist"
       },
       "msg":{
          "isConnected":"bool",
          "relativePosition":{
             "y":"float"
          },
          "speed":{
             "activeAcceleration":{
                "y":"bool"
             },
             "acceleration":{
                "y":"float"
             },
             "speed":{
                "y":"float"
             }
          }
       }
    }
   ```
- Trolley
   ```JSON
    {
       "meta":{
          "topic":"crane/components/trolley/state",
          "active":"bool",
          "component":"trolley"
       },
       "msg":{
          "relativePosition":{
             "x":"float"
          },
          "speed":{
             "activeAcceleration":{
                "x":"bool"
             },
             "acceleration":{
                "x":"float"
             },
             "speed":{
                "x":"float"
             }
          }
       }
    }
   ```
- Boom
    ```JSON
    {
       "meta":{
          "topic":"crane/components/boom/state",
          "isActive":"bool",
          "component":"boom"
       },
       "msg":{
          "relativePosition":{
             "x":"float",
             "y":"float"
          },
          "speed":{
             "activeAcceleration":{
                "x":"bool",
                "y":"bool"
             },
             "acceleration":{
                "x":"float",
                "y":"float"
             },
             "speed":{
                "x":"float",
                "y":"float"
             }
          }
       }
    }
    ```
- Gantry
   ```JSON
    {
       "meta":{
          "topic":"crane/components/gantry/state",
          "active":"bool",
          "component":"gantry"
       },
       "msg":{
          "absolutePosition":{
             "z":"float"
          },
          "speed":{
             "activeAcceleration":{
                "z":"bool"
             },
             "acceleration":{
                "z":"float"
             },
             "speed":{
                "z":"float"
             }
          }
       }
    }
   ```
#### Crane State Messages
   ```JSON
   {
      "meta": {
         "topic": "crane/components/Gantry/state",
         "active": bool
      },
      "msg": {
         "absolutePosition": {
            "z": float
        },
        "speed": {
          "activeAcceleration": {
            "z": bool
          },
          "acceleration": {
            "z": float
          },
          "speed": {
            "z": float,
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
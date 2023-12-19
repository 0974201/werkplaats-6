# Conventions

Hard guidelines for maintaining/operating/developing this project.

## Data

- Metrics
  - Dimensions = meters
  - Time = Seconds
- Floats have 2 decimal points

## MQTT messages

### Topics

each message will have a topic property, this is equal to its MQTT topic

- Command Emergency Button Messages
  - = Command to stop entire system
  - topic = "meta/emergency_button"
  - sender = controller
- Command Messages
  - = Command to specific component
  - topic = "crane/components/_component_/command"
  - sender = controller
  - command syntax:
    0 => do nothing
    1 => go up/right/forward
    2 => go down/left/backward
- Container State Messages
  - = Current state from this container
  - topic = "containers/_container_ID_/state"
  - sender = _this_container_
- Component State Messages
  - = Current state from this component
  - topic = "crane/components/_component_/state"
  - sender = _this_component_
- Crane State Message
  - = Current state of all components of the crane
  - topic = "crane/state"
  - sender = crane
- Error Messages
  - = Error messages
  - topic = "meta/errors"
  - sender = _any_microservice_

### Messages formats

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

#### Emergency Button Messages

```JSON
{
   "meta":{
      "topic":"meta/emergency_button"
   },
   "msg":{
      "isPressed":"bool"
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
      "command":"int"
   }
}
```

#### Container Messages

```JSON
{
   "meta":{
      "topic":"containers/id/state",
      "component":"container"
   },
   "msg":{
      "id":"int",
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
        "rotation": "float",
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
- Spreader
  ```JSON
   {
      "meta":{
         "topic":"crane/components/spreader/state",
         "id":"string",
         "component":"spreader"
      },
      "msg":{
         "isConnected": true,
         "position":{
            "x": 0.0,
            "y": 0.0,
            "z": 0.0
         },
         "attachedContainer":{
            "id":"string",
            "position":{
               "x": 0.0,
               "y": 0.0,
               "z": 0.0
            },
            "weight": 0.0
         }
      }
   }
  ```

#### Crane State Messages

```JSON
{
  "meta": {
          "topic":"crane/state",
          "isActive":"bool"
  },
  "absolutePosition": {
    "x": "float",
    "y": "float",
    "z": "float"
  },
  "components": [
    {
      "component": "hoist",
      "isActive": "bool",
      "isConnected": "bool",
      "absolutePosition": {
          "x": "float",
          "y": "float",
          "z": "float"
      },
      "speed": {
          "activeAcceleration": {
            "y": "bool"
          },
          "acceleration": {
            "y": "float"
          },
          "speed": {
            "y": "float"
          }
      }
    },
    {
      "component": "trolley",
      "isActive": "bool",
      "absolutePosition": {
        "x": "float",
        "y": "float",
        "z": "float"
       },
       "acceleration": {
         "y": "float"
       },
       "speed": {
         "y": "float"
       }
   }
 },
 {
   "component": "trolley",
   "isActive": "bool",
   "absolutePosition": {
     "x": "float",
     "y": "float",
     "z": "float"
    },
   "speed": {
     "activeAcceleration": {
       "x": "bool"
     },
     "acceleration": {
       "x": "float"
     },
     "speed": {
       "x": "float"
     }
   }
 },
 {
   "component": "boom",
   "isActive": "bool",
   "absolutePosition": {
     "x": "float",
     "y": "float",
     "z": "float"
   },
   "speed": {
     "activeAcceleration": {
       "x": "bool",
       "y": "bool"
     },
     "acceleration": {
       "x": "float",
       "y": "float"
     },
     "speed": {
       "X": "float",
       "y": "float"
     }
   }
 },
 {
   "component": "gantry",
   "isActive": "bool",
   "absolutePosition": {
     "x": "float",
     "y": "float",
     "z": "float"
   },
   "speed": {
     "activeAcceleration": {
       "z": "bool"
     },
     "acceleration": {
      "z": "float"
     },
     "speed": {
       "z": "float"
     }
   }
 },
 {
   "component": "spreader",
   "isActive": true,
   "absolutePosition": {
      "x": 0.0,
      "y": 0.0,
      "z": 0.0
   },
   "speed": {
      "activeAcceleration": {
         "x": false
      },
      "acceleration": {
         "x": 0.0
      },
      "speed": {
         "x": 0.0
      }
    },
    {
      "component": "gantry",
      "isActive": "bool",
      "absolutePosition": {
        "x": "float",
        "y": "float",
        "z": "float"
      },
      "speed": {
        "activeAcceleration": {
          "z": "bool"
        },
        "acceleration": {
         "z": "float"
        },
        "speed": {
          "z": "float"
        }
      }
    }
  ],
  "container": {
      "id":"int",
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
  },
  "commands": [
    {
      "target": "str",
      "command": "int"
    }
  ]
}
```

#### Error Messages

```JSON
{
   "meta": {
      "topic": "meta/errors"
   },
   "msg": {
      "type": "str",
      "microservice": "str",
      "error": "str"
   }
}
```

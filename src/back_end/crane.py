from broker.client import Client
from threading import Thread, ThreadError
import asyncio
import ast


class Crane:
    def __init__(self, frequency):
        self.frequency = frequency
        self.client = Client("crane")
        self.output = {
    "meta": {
        "topic": "crane/state",
        "isActive": True
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
            "isConnecting": "bool",
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
        }
    ],
    "container": {
        "id": "int",
        "isConnected": "bool",
        "absolutePosition": {
            "x": "float",
            "y": "float",
            "z": "float"
        },
        "speed": {
            "speed": {
                "x": "float",
                "y": "float",
                "z": "float"
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
        self.component_states =
        self.component_commands =
        self.container_state =
        self.emergency_button_state =

    def set_callbacks(self):
      def on_emergency_button(client, userdata, msg):
        i

class
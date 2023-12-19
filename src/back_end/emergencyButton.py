import paho.mqtt.client as mqtt
from paho import mqtt
import broker.client
import json

# class for the emergency button
class EmergencyButton:
    def __init__(self, topic, sender):
        self.client = broker.client.Client("hoist", "crane/components/hoist/state", 0) 
        self.client.serve()
        self.topic = topic # topic of emergency message
        self.sender = sender # sender of emergency message

    # method to stop the system when the emergency button is pressed
    def stop_system(self):
        # print message
        print(f"Emergency button pressed! Stopping the system for topic '{self.topic}' by sender '{self.sender}'.")
        # create message
        message = json.dumps({"command": "stop_system"})
        # publish it to the topic 
        self.client.publish(self.topic, message)

    def simulate(self):
        data = {
            "meta":
                {
                    "topic": "crane/components/emergencyButton/state",
                    "isActive": True,
                    "component": "emergencyButton"
                },
            "msg": {
                "isConnected": False,
                "relativePosition": {
                    "y": round(random.uniform(0.0, 10.0), 2)
                },
                "speed": {
                    "activeAcceleration": {
                        "y": True
                    },
                    "acceleration": {
                        "y": round(random.uniform(0.0, 10.0), 2)
                    },
                    "speed": {
                        "y": round(random.uniform(0.0, 10.0), 2)
                    }
                }
            }
        }
        self.client.publish("crane/components/emergencyButton/state", data)
        self.client.disconnect()

EmergencyButton()

import paho.mqtt.client as mqtt
from paho import mqtt
import broker.client
import json

# class for the emergency button
class EmergencyButton:
    def __init__(self, isActive):
        self.client = broker.client.Client("emergencyButton", [("crane/components/emergencyButton/state")], 0)
        self.client.serve()
        self.isActive = isActive
        self.topic = "meta/emergency_button" # topic of emergency message
        self.sender = "controller" # sender of emergency message

    # method to stop the system when the emergency button is pressed
    def stop_system(self):
        # print message
        print(f"Emergency button pressed! Stopping the system for topic '{self.topic}' by sender '{self.sender}'.")
        if self.isActive:
            self.emergencyData()

    def emergencyData(self):
        data = {
            "meta":
                {
                    "topic": "crane/components/emergencyButton/state",
                    "isActive": self.isActive,
                    "component": "emergencyButton"
                },
            "msg": {
                "command": "stop_system",
                "topic": self.topic,
                "sender": self.sender
                }
            }
        self.client.publish("crane/components/emergencyButton/state", data)
        self.client.disconnect()

emergency = EmergencyButton(True)
emergency.stop_system()

import paho.mqtt.client as mqtt
from paho import mqtt
import broker.client
import time
import json

# class for the emergency button
class EmergencyButton:
    def __init__(self, isPressed):
        self.client = broker.client.Client("emergencyButton", [("meta/emergency_button")], 0)
        self.client.serve()
        self.isPressed = isPressed
        self.topic = "meta/emergency_button" # topic of emergency message
        self.sender = "controller" # sender of emergency message
        self.loop = True

    def on_message(self, client, userdata, msg):
        print("Message received: ", msg.payload)
        data_dict = json.loads(msg.payload.decode('utf-8'))
        keys = data_dict.get('msg', {}).get('key') 
        self.stop_system(keys)


    # method to stop the system when the emergency button is pressed
    def stop_system(self, keys = None):
        while self.loop:
            if keys == 1:
                if self.isPressed:
                    print("Emergency button already pressed!")
                else:
                    # print message
                    print(f"Emergency button pressed! Stopping the system for topic '{self.topic}' by sender '{self.sender}'.")
                    self.emergencyData()
            elif keys == -1:
                if self.isPressed:
                    self.Pressed = False
                    print("Emergency button resetted.")
                else:
                    print("Emergency button is not active.")
            else:
                print("No key pressed!")
                time.sleep(1)
                

    def emergencyData(self):
        data = {
            "meta":
                {
                    "topic": self.topic
                },
            "msg": {
                "isPressed": self.isPressed,
                }
            }
        self.client.publish("meta/emergency_button", data)
        self.client.disconnect()

emergency = EmergencyButton(True)
emergency.stop_system()

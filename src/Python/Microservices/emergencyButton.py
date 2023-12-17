import paho.mqtt.client as mqtt
from paho import mqtt
import json

# class for the emergency button
class EmergencyButton:
    def __init__(self, mqtt_broker, topic, sender):
        self.topic = topic # topic of emergency message
        self.sender = sender # sender of emergency message

        # mqtt connection
        self.client = mqtt.Client()
        self.client.connect(mqtt_broker)
        self.client.loop_start()

    # method to stop the system when the emergency button is pressed
    def stop_system(self):
        # print message
        print(f"Emergency button pressed! Stopping the system for topic '{self.topic}' by sender '{self.sender}'.")
        # create message
        message = json.dumps({"command": "stop_system"})
        # publish it to the topic
        self.client.publish(self.topic, message)

    # stop the mqtt client
    def stop_mqtt(self):
        self.client.loop_stop()
        self.client.disconnect()


import paho.mqtt.client as mqtt
import json

class Spreader:
    def __init__(self, client, position=(0, 0, 0)):
        self.client = client
        self.position = position
        self.attached_container = None

    def move_to_position(self, new_position):
        self.position = new_position
        self.client.publish("spreader/position", json.dumps({"position": self.position}))

    def attach_container(self, container):
        if self.position == container.position and not self.attached_container:
            self.attached_container = container
            container.is_lifted = True
            self.client.publish("spreader/action", json.dumps({"action": "attach", "container_id": id(container)}))

    def release_container(self):
        if self.attached_container:
            self.client.publish("spreader/action", json.dumps({"action": "release", "container_id": id(self.attached_container)}))
            self.attached_container.is_lifted = False
            self.attached_container = None
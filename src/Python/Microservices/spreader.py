
import paho.mqtt.client as mqtt
import json

# init the Spreader with a client for MQTT communication and an init position
class Spreader:
    def __init__(self, client, position=(0, 0, 0)):
        self.client = client
        self.position = position
        self.attached_container = None # init with no attached container
        self.errors = [] # list to store error messages

    # move the Spreader to a new position
    def move_to_position(self, new_position):
        self.position = new_position
        self.client.publish("spreader/position", json.dumps({"position": self.position}))

    # attach a container to the Spreader if conditions are met
    def attach_container(self, container):
        if self.position == container.position and not self.attached_container:
            self.attached_container = container
            container.is_lifted = True
            self.client.publish("spreader/action", json.dumps({"action": "attach", "container_id": id(container)}))

    # release the attached container
    def release_container(self):
        if self.attached_container:
            self.client.publish("spreader/action", json.dumps({"action": "release", "container_id": id(self.attached_container)}))
            self.attached_container.is_lifted = False
            self.attached_container = None

    # stop the Spreader's movement and send a status update
    def stop(self):
        self.is_moving = False
        self.send_status_update()

    # publish the current status of the Spreader via MQTT
    def send_status_update(self):
        status = self.get_status()
        self.client.publish("spreader/status", json.dumps(status))

    # get the current status of the Spreader, including position and attached container information
    def get_status(self):
        return {
            "position": self.position,
            "attached_container": id(self.attached_container),
            "errors": self.errors
        }
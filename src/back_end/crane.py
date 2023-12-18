
import paho.mqtt.client as mqtt
import json

# import components
from hoist import Hoist
from trolley import Trolley
from boom import Boom
from gantry import Gantry

# init crane position, maximum dimensions, load, and error list
class Crane:
    def __init__(self, mqtt_broker, x=0, y=0, z=0, max_height=50, max_arm_length=35, max_load=20):
        
        # set init position and maximum capacities
        self.x = x
        self.y = y
        self.z = z
        self.max_height = max_height
        self.max_arm_length = max_arm_length
        self.max_load = max_load
        self.is_moving = False
        self.load = 0
        self.errors = []

        # init components 
        self.hoist= Hoist()
        self.trolley = Trolley()
        self.boom = Boom()
        self.gantry = Gantry()
        self.spreader = Spreader()

        # connect to the mqtt broker and start the mqtt loop
        self.client = mqtt.Client()
        self.client.on_connect = self.on_connect
        self.client.on_message = self.on_message
        self.client.connect(mqtt_broker)
        self.client.loop_start()

    # subscribe to topics
    def on_connect(self, client, userdata, flags, rc):
        if rc == 0:  # Succesvol verbonden
            client.subscribe("containers/id/state")
            client.subscribe("crane/components/hoist/state")
            client.subscribe("crane/components/trolley/state")
            client.subscribe("crane/components/boom/state")
            client.subscribe("crane/components/gantry/state")

    def on_message(self, client, userdata, msg):
        # handle messages from container state topic
        if msg.topic == "containers/id/state":
            container_id = msg.payload.decode("utf-8")
            if container_id == "connected":
                self.load = 1
            elif container_id == "disconnected":
                self.load = 0

        # handle messages from crane component state topics
        elif msg.topic == "crane/components/hoist/state":
            hoist_state = json.loads(msg.payload.decode("utf-8"))
            if hoist_state["isActive"]:
                self.is_moving = True
            else:
                self.is_moving = False

        elif msg.topic == "crane/components/trolley/state":
            trolley_state = json.loads(msg.payload.decode("utf-8"))
            if trolley_state["isActive"]:
                self.is_moving = True
            else:
                self.is_moving = False

        elif msg.topic == "crane/components/boom/state":
            boom_state = json.loads(msg.payload.decode("utf-8"))
            if boom_state["isActive"]:
                self.is_moving = True
            else:
                self.is_moving = False

        elif msg.topic == "crane/components/gantry/state":
            gantry_state = json.loads(msg.payload.decode("utf-8"))
            if gantry_state["isActive"]:
                self.is_moving = True
            else:
                self.is_moving = False

    # Change: Check whether the crane is moving and check overload condition
    def move(self, x_position_change, y_position_change, z_position_change):
        if self.is_moving and self.load > self.max_load:
            # Change: If the crane is overloaded, add an error message and stop the movement
            self.errors.append("Overload: Movement stopped")
            self.stop()
            return

       # update crane position within allowed limits
        new_x_position = min(max(self.x + x_position_change, 0), self.max_arm_length)
        new_y_position = min(max(self.y + y_position_change, 0), self.max_height)
        new_z_position = min(max(self.z + z_position_change, 0), self.max_load)

        if new_x_position != self.x or new_y_position != self.y or new_z_position != self.z:
            self.x = new_x_position
            self.y = new_y_position
            self.z = new_z_position
            self.is_moving = True
            self.send_status_update()

    # stop the crane's movement and update status
    def stop(self):
        self.is_moving = False
        self.send_status_update()

    # retrieve the crane's overall status and component status
    def send_status_update(self):
        # Send crane status update to MQTT broker
        self.client.publish("crane/state", json.dumps({
            "meta": {
                "topic": "crane/state",
                "isActive": self.is_moving
            },
            "absolutePosition": {
                "x": self.x,
                "y": self.y,
                "z": self.z
            },
            "container": {
                "id": self.load > 0,  # containerid
                "isConnected": self.load > 0  # connection
            },
            "components": [
                {
                    "component": "hoist",
                    "isActive": self.hoist.isActive,
                    "isConnected": self.hoist.isConnected
                },
                {
                    "component": "trolley",
                    "isActive": self.trolley.isActive,
                    "isConnected": self.trolley.isConnected
                },
                {
                    "component": "boom",
                    "isActive": self.boom.isActive,
                    "isConnected": self.boom.isConnected
                },
                {
                    "component": "gantry",
                    "isActive": self.gantry.isActive,
                    "isConnected": self.gantry.isConnected
                }
            ]
        }))

    # Send the current status to an MQTT topic
    # def send_status_update(self):
    #     status = self.get_status()
    #     self.client.publish("crane/state", json.dumps(status))

    # # Properly close the MQTT client connection
    def close(self):
        self.client.loop_stop()
        self.client.disconnect()

 # Load cargo, ensuring not to exceed max load
    # def load_cargo(self, weight):
    #     if weight <= self.max_load - self.load:
    #         self.load += weight
    #         self.send_status_update()
    #     else:
    #         self.errors.append("Error: Overload")

    # # Unload the cargo
    # def unload_cargo(self):
    #     self.load = 0
    #     self.send_status_update()

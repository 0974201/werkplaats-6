import paho.mqtt.client as mqtt
import json

# Initialize crane position, maximum dimensions, load, and error list
class Crane:
    def __init__(self, mqtt_broker, x=0, y=0, z=0, max_height=50, max_arm_length=35, max_load=20):
        
        self.x = x
        self.y = y
        self.z = z
        self.max_height = max_height
        self.max_arm_length = max_arm_length
        self.max_load = max_load
        self.is_moving = False
        self.load = 0
        self.errors = []

        # MQTT client setup
        self.client = mqtt.Client()
        self.client.connect(mqtt_broker)
        self.client.loop_start()

    # Change: Check whether the crane is moving and check overload condition
    def move(self, x_position_change, y_position_change, z_position_change):
        if self.is_moving and self.load > self.max_load:
            # Change: If the crane is overloaded, add an error message and stop the movement
            self.errors.append("Overload: Movement stopped")
            self.stop()
            return

       # Update crane position within allowed limits
        new_x_position = min(max(self.x + x_position_change, 0), self.max_arm_length)
        new_y_position = min(max(self.y + y_position_change, 0), self.max_height)
        new_z_position = min(max(self.z + z_position_change, 0), self.max_load)

        if new_x_position != self.x or new_y_position != self.y or new_z_position != self.z:
            self.x = new_x_position
            self.y = new_y_position
            self.z = new_z_position
            self.is_moving = True
            self.send_status_update()

    # Stop crane movement
    def stop(self):
        self.is_moving = False
        self.send_status_update()

    # Change: Return the current status of the crane with separate position values
    def get_status(self):
        return {
            "x_position": self.x,
            "y_position": self.y,
            "z_position": self.z,
            "moving": self.is_moving,
            "load": self.load,
            "errors": self.errors
        }

    # Load cargo, ensuring not to exceed max load
    def load_cargo(self, weight):
        if weight <= self.max_load - self.load:
            self.load += weight
            self.send_status_update()
        else:
            self.errors.append("Error: Overload")

    # Unload the cargo
    def unload_cargo(self):
        self.load = 0
        self.send_status_update()

    # Send the current status to an MQTT topic
    def send_status_update(self):
        status = self.get_status()
        self.client.publish("crane/status", json.dumps(status))

    # Properly close the MQTT client connection
    def close(self):
        self.client.loop_stop()
        self.client.disconnect()

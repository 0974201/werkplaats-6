import paho.mqtt.client as mqtt
import json

class Boom:
    def __init__(self, isActive, mqtt_broker, rotationZ):
        self.isActive = isActive
        self.rotationZ = rotationZ  # rotation angle of the boom Z axis

        # MQTT connection
        self.client = mqtt.Client()
        try:
            self.client.connect(mqtt_broker)
            self.client.loop_start()
        except Exception as e:
            print(f"Failed to connect to MQTT broker: {e}")

    def update_rotation(self, deltaZ):
        # Validate deltaZ input
        if not isinstance(deltaZ, (int, float)):
            print("Invalid rotation value. Rotation must be a number.")
            return

        # Update and normalize rotation angle
        self.rotationZ = (self.rotationZ + deltaZ) % 180
        self.publish_status()

    def publish_status(self):
        status = {
            "isActive": self.isActive,
            "rotation": self.rotationZ
        }
        try:
            self.client.publish("boom/state", json.dumps(status))
        except Exception as e:
            print(f"Failed to publish status: {e}")

    def stop_mqtt(self):
        try:
            self.client.loop_stop()
            self.client.disconnect()
        except Exception as e:
            print(f"Failed to stop MQTT client: {e}")

    def get_rotation(self):
        return self.rotationZ

# 0 is the initial rotationangle of the boom
boom = Boom(True, 'mqtt_broker_address', 0)
# rotation angle of the boom is increased by 15 degrees from its current position.
boom.update_rotation(15)

# method for getting the cordinates
#   def get_coordinates(self):
#     return (self.positionX, self.positionY)

# # method for getting the rotation
#   def get_rotation(self):
#     return self.rotationZ



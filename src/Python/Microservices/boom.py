import math
import paho.mqtt.client as paho
from paho import mqtt
import json

# define class boom 
class Boom:
  # constructor method for the class
  def __init__(self, isActive, mqtt_broker, positionX, positionY, rotationZ, speed):
    # assign the parameters to the instance attributes
    self.isActive = isActive 
    self._speed = speed  
    self.positionX = positionX 
    self.positionY = positionY 
    self.rotationZ = rotationZ # rotationangle of the boom Z axis
    # self.angle = angle
    
    # mqtt connection
    self.client = mqtt.Client()
    try:
        self.client.connect(mqtt_broker)
        self.client.loop_start()
    except Exception as e:
        print(f"Failed to connect to MQTT broker: {e}")

  # update the position of the boom x moves right and left position y moves up and down
  def update_position(self, deltaX, deltaY):
    try:
        self._positionX += deltaX
        self._positionY += deltaY
        self.publish_status()
    except Exception as e:
        print(f"Failed to update position: {e}")

  # update the rotation of the boom
  def update_rotation(self, deltaZ):
    try:
        self._rotationZ += deltaZ # update the rotation angle with deltaZ
        self._rotationZ %= 360  # Normalize angle to 0-360
        self.publish_status() # publish new status
    except Exception as e:
        print(f"Failed to update rotation: {e}")

  # publish the current status of the boom to an mqtt topic
  def publish_status(self):
    try:
        status = {
            "isActive": self.isActive,
            "position": {"x": self._positionX, "y": self._positionY},
            "rotation": self._rotationZ,
            "speed": self._speed
        }
        self.client.publish("boom/state", json.dumps(status))
    except Exception as e:
        print(f"Failed to publish status: {e}")

  # stop the mqtt client
  def stop_mqtt(self):
      self.client.loop_stop()
      self.client.disconnect

# template_mqtt
# boom = Boom('mqtt_broker_address', True, 0, 0, 90, 5)
# boom.update_position(10, 5)
# boom.update_rotation(15)

# method for getting the cordinates
#   def get_coordinates(self):
#     return (self.positionX, self.positionY)

# # method for getting the rotation
#   def get_rotation(self):
#     return self.rotationZ



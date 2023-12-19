# imports
import paho.mqtt.client as mqtt
import json

# define class boom 
class Boom:
  # constructor method for the class
  def __init__(self, isActive, mqtt_broker, rotationZ):
    # assign the parameters to the instance attributes
    self.isActive = isActive 
    self.rotationZ = rotationZ # rotation angle of the boom Z axis
    
  # mqtt connection
    self.client = mqtt.Client()
    self.client.connect(mqtt_broker)
    self.client.loop_start()

  # update the rotation of the boom
  def update_rotation(self, deltaZ):
    self.rotationZ += deltaZ # update the rotation angle with deltaZ
    self.rotationZ %= 360  # Normalize angle to 0-360
    self.publish_status() # publish new status

  # publish the current status of the boom to an mqtt topic
  def publish_status(self):
    status = {
        "isActive": self.isActive,
        "rotation": self.rotationZ
    }
    self.client.publish("boom/state", json.dumps(status))

  # stop the mqtt client
  def stop_mqtt(self):
      self.client.loop_stop()
      self.client.disconnect

# template_mqtt
# boom = Boom('mqtt_broker_address', True, 0, 0, 90, 5)
# boom.update_rotation(15)

# method for getting the cordinates
#   def get_coordinates(self):
#     return (self.positionX, self.positionY)

# # method for getting the rotation
#   def get_rotation(self):
#     return self.rotationZ



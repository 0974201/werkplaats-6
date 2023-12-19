# imports
import broker.client
import json

# define class boom 
class Boom:
  # constructor method for the class
  def __init__(self, isActive, rotationZ, speed, angle):
    self.client = broker.client.Client("boom", [("crane/components/boom/state")], 0) 
    self.client.serve()
    # assign the parameters to the instance attributes
    self.isActive = isActive 
    self.rotationZ = rotationZ # rotation angle of the boom Z axis
    self.speed = speed
    self.angle = angle
    


  # update the rotation of the boom
  def update_rotation(self, rotationZ, speed, angle):
    while rotationZ < angle:
      # Update rotationZ by adding the speed
      rotationZ += speed
      # Print the current rotationZ
      print("rotationZ =", rotationZ)
      self.boomData()


  def boomData(self):
    data = {
        "meta":
            {
                "topic": "crane/components/boom/state",
                "isActive": True,
                "component": "boom"
            },
        "msg": {
            "Rotation": {
                "Z": self.rotationZ
            },
        }
    }
    self.client.publish("crane/components/boom/state", data)
    self.client.disconnect()

# Boom()

# template_mqtt
# boom = Boom('mqtt_broker_address', True, 0, 0, 90, 5)
# boom.update_rotation(15)

# method for getting the cordinates
#   def get_coordinates(self):
#     return (self.positionX, self.positionY)

# # method for getting the rotation
#   def get_rotation(self):
#     return self.rotationZ


boom = Boom(True, 0, 5, 90)
boom.update_rotation(0, 5, 90)
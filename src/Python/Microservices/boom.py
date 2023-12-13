import math
import paho.mqtt.client as paho
from paho import mqtt
import json

# define class boom 
class Boom:
  # constructor method for the class
  def __init__(self, isActive, Speed, positionX, positionY, rotationZ, angle):
    # assign the parameters to the instance attributes
    self.isActive = isActive 
    self.Speed = Speed 
    self.positionX = positionX 
    self.positionY = positionY 
    self.rotationZ = rotationZ 
    self.angle = angle
    
# method for getting the cordinates
  def get_coordinates(self):
    return (self.positionX, self.positionY)

# method for getting the rotation
  def get_rotation(self):
    return self.rotationZ



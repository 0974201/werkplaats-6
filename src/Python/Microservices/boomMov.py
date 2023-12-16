import math
import paho.mqtt.client as paho
from paho import mqtt
import json

# Define class boom 
class Boom:
  def __init__(self, isActive, Speed, positionX, positionY, rotationZ):
    self.isActive = isActive 
    self.Speed = Speed 
    self.positionX = positionX 
    self.positionY = positionY 
    self.rotationZ = rotationZ 
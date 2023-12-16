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

  # method to move the boom up or down
  def move(self, direction):
    # Check if the boom is active
    if self.isActive:
      # Check the direction of the movement
      if direction == "up":
        # Increase the vertical position by the speed times the sine of the angle
        self.positionY += self.Speed * math.sin(math.radians(self.rotationZ))
      elif direction == "down":
        # Decrease the vertical position by the speed times the sine of the angle
        self.positionY -= self.Speed * math.sin(math.radians(self.rotationZ))
      else:
        # Invalid direction
        print("Invalid direction. Please enter 'up' or 'down'.")
    else:
      # boom is not active
      print("The boom is not active. Please activate it first.")

  # method to rotate the boom
  def rotate(self, angle):
    if self.isActive:
      # Add the angle to the rotation angle
      self.rotationZ += angle
    else:
      print("The boom is not active. Please activate it first.")

  # method to print the current state of the boom
  def print_state(self):
    # Print the attributes of the boom
    print("isActive:", self.isActive)
    print("Speed:", self.Speed)
    print("positionX:", self.positionX)
    print("positionY:", self.positionY)
    print("rotationZ:", self.rotationZ)

# instance of the boom class
boom = Boom(True, 50, 100, 50, 0)
# test the methods of the boom class
boom.print_state() # Print the initial state
boom.rotate(90) # Rotate the boom 
boom.print_state() # Print the updated state
boom.move("up") # Move the boom up
boom.print_state() 
boom.move("down") 
boom.print_state() 

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
        # Set the initial speed to zero
        speed = 0
        # Set the acceleration to a positive value
        acc = 10
        # Set the time interval to a small value
        dt = 0.1
        # Loop until the speed reaches the maximum speed
        while speed < self.Speed:
          # Increase the speed by the acceleration times the time interval
          speed += acc * dt
          # Increase the vertical position by the speed times the sine of the angle times the time interval
          self.positionY += speed * math.sin(math.radians(self.rotationZ)) * dt
          # Print the current speed, position and angle
          print("Speed:", speed, "positionY:", self.positionY, "rotationZ:", self.rotationZ)
        # Loop until the speed reaches zero
        while speed > 0:
          # Decrease the speed by the acceleration times the time interval
          speed -= acc * dt
          # Increase the vertical position by the speed times the sine of the angle times the time interval
          self.positionY += speed * math.sin(math.radians(self.rotationZ)) * dt
          # Print the current speed, position and angle
          print("Speed:", speed, "positionY:", self.positionY, "rotationZ:", self.rotationZ)
          # Update the angle by the speed times the cosine of the angle times the time interval divided by the horizontal position
          self.rotationZ += speed * math.cos(math.radians(self.rotationZ)) * dt / self.positionX
      elif direction == "down":
        # Set the initial speed to zero
        speed = 0
        # Set the acceleration to a negative value
        acc = -10
        # Set the time interval to a small value
        dt = 0.1
        # Loop until the speed reaches the minimum speed
        while speed > self.Speed:
          # Decrease the speed by the acceleration times the time interval
          speed += acc * dt
          # Decrease the vertical position by the speed times the sine of the angle times the time interval
          self.positionY -= speed * math.sin(math.radians(self.rotationZ)) * dt
          # Print the current speed, position and angle
          print("Speed:", speed, "positionY:", self.positionY, "rotationZ:", self.rotationZ)
        # Loop until the speed reaches zero
        while speed < 0:
          # Increase the speed by the acceleration times the time interval
          speed -= acc * dt
          # Decrease the vertical position by the speed times the sine of the angle times the time interval
          self.positionY -= speed * math.sin(math.radians(self.rotationZ)) * dt
          # Print the current speed, position and angle
          print("Speed:", speed, "positionY:", self.positionY, "rotationZ:", self.rotationZ)
          # Update the angle by the speed times the cosine of the angle times the time interval divided by the horizontal position
          self.rotationZ -= speed * math.cos(math.radians(self.rotationZ)) * dt / self.positionX
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
boom = Boom(True, 20, 100, 50, 0)
# test the methods of the boom class
boom.print_state() # Print the initial state
boom.rotate(60) # Rotate the boom by 60 degrees
boom.print_state() # Print the updated stat
boom.move("up") # Move the boom up
boom.print_state() # Print the final state

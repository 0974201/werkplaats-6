import math

class Boom:
    def __init__(self, isActive, positionX, positionY, rotationZ, speed):
        self.isActive = isActive
        self.speed = speed 
        self.positionX = positionX 
        self.positionY = positionY 
        self.rotationZ = rotationZ 
        

    def get_coordinates(self):
        return (self.positionX, self.positionY)

    def get_rotation(self):
        return self.rotationZ

    def get_speed(self):
        return self.speed



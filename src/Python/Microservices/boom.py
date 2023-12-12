import math

class Boom:
    def __init__(self, isActive, posX, posY, rotZ, speed):
        self.isActive = isActive
        self.posX = posX 
        self.posY = posY 
        self.rotZ = rotZ 
        self.speed = speed 

    def get_coordinates(self):
        return (self.posX, self.posY)

    def get_rotation(self):
        return self.rotZ

    def get_speed(self):
        return self.speed



import math

class Boom:
    def __init__(self, posX, posY, rotZ, speed):
        self.posX = posX 
        self.posY = posY 
        self.rotZ = rotZ 
        self.speed = speed 

    def get_coordinates(self):
        return (self.posX, self.posY)

    def get_rotation(self):
        return self.rotZ

    def set_coordinates(self, posX, posY, rotZ):
        self.posX = posX
        self.posY = posY
        self.rotZ = rotZ

    def set_rotation(self, rotZ):
        self.rotZ = rotZ

    def get_speed(self):
        return self.speed

    def set_speed(self, speed):
        self.speed = speed


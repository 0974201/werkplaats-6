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
        self.rotationZ = rotationZ  # rotation angle of the boom Z axis
        self.speed = speed
        self.angle = angle

    # update the rotation of the boom
    def update_rotation(self):
        while self.rotationZ < self.angle:
            self.boomData()
            # Update rotationZ by adding the speed
            self.rotationZ += self.speed
            # Print the current rotationZ
            print("rotationZ =", self.rotationZ)
            # Call boomData after the loop completes
        self.client.disconnect()
        

    def boomData(self):
        data = {
            "meta": {
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

# Create an instance of Boom
boom = Boom(True, 0, 5, 90)
# Call the update_rotation method
boom.update_rotation()

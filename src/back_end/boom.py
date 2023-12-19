# imports
import broker.client
import time
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
        self.loop = True

    def on_message(self, client, userdata, msg):
        print("Message received: ", msg.payload)
        data_dict = json.loads(msg.payload.decode('utf-8'))
        self.is_active = data_dict.get('meta', {}).get('isActive')
        keys = data_dict.get('msg', {}).get('key') 
        # oldRotation = data_dict.get('msg', {}).get('rotationZ') 

        self.update_rotation(keys)

    # update the rotation of the boom
    def update_rotation(self, keys=None):
        while self.loop:
            if keys == 2:
                while self.rotationZ > 0:
                    # Update rotationZ by subtracting the speed
                    self.rotationZ -= self.speed
                    # Print the current rotationZ
                    print("rotationZ =", self.rotationZ)
                    # Call boomData
                    self.boomData()
                    time.sleep(1)
            elif keys == 1:
                while self.rotationZ < self.angle:
                    # Call boomData
                    self.boomData()
                    # Update rotationZ by adding the speed
                    self.rotationZ += self.speed
                    # Print the current rotationZ
                    print("rotationZ =", self.rotationZ)
                    time.sleep(1)
            else:
                print("No key Pressed")
                time.sleep(1)
            # self.client.disconnect()
                    
        

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
boom = Boom(True, 0, 5, 75)
# Call the update_rotation method
boom.update_rotation()

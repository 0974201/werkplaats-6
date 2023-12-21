import broker.client
import random
import time
import json
import math
import keyboard

class Hoist:
    def __init__(self):
        self.loop = True
        self.client = broker.client.Client("hoist", [("crane/components/hoist/command", 0),("trolly", 0),("gantry", 0),("container", 0),("dashboard", 0)])
        self.client.serve()
        self.is_active = True
        self.acceleration = 1
        self.frequency = 0.1
        self.hoist_y = 0
        self.looper()

    def on_message(self, client, userdata, msg):
        print("Message received: ", msg.payload)
        data_dict = json.loads(msg.payload.decode('utf-8'))  
        self.proccesData(data_dict)
    
    broker.client.Client.on_message = on_message
    
    def proccesData(self, data_dict):
        isConnected = data_dict.get('msg', {}).get('isConnected')
        is_active = data_dict.get('meta', {}).get('isActive')
        command = data_dict.get('msg', {}).get('command', {}).get('hoist')
        containerX = data_dict.get('msg', {}).get('containerX') 
        containerY = data_dict.get('msg', {}).get('containerY') 
        containerZ = data_dict.get('msg', {}).get('containerZ')
        hoistX = data_dict.get('msg', {}).get('TrollyX')
        hoistZ = data_dict.get('msg', {}).get('Gantryz')
        
        print(command)
        self.movement(command)
        self.container_connect(isConnected, containerX, containerY, containerZ, hoistX, hoistZ)

    

    def looper(self):
        while self.loop:
            pass

    def movement(self, command):
        while self.loop == True:
            time.sleep(self.frequency)
            if command == 1:
                
                self.acceleration = self.acceleration * 1.1
                speed = 0.83 * self.acceleration
                print(f"speed: {speed}")

                if speed >= 2.5:
                    speed = 2.5
                move_hoist_y = speed
                print(f"move_hoist_y: {move_hoist_y}")
                
                self.position(move_hoist_y)

            elif command == -1:
                print(f"command: {command}")
                self.acceleration = self.acceleration * 1.1
                speed = -0.83 * self.acceleration
                print(f"speed: {speed}")
                
                if speed <= -2.5:
                    speed = -2.5
                # print(speed)
                move_hoist_y = speed
                print(f"move_hoist_y: {move_hoist_y}")
                
                self.position(move_hoist_y)
            else:
                print("no_keypress")

    def position(self, speed):
        move_hoist_y = speed
        self.hoist_y += move_hoist_y
        pos = self.hoist_y
        max_pos = 40
        min_pos = 0

        if pos > max_pos:
            pos = 40
        elif pos < min_pos:
            pos = 0

        print(f"pos: {pos}")
        self.Hoist_data(speed, pos)

    def container_connect(self, isConnected, containerX,containerY,containerZ ,hoist_y, hoistX, hoistZ):
            # testdata to check container
            containerX = 10
            containerY = 10
            containerZ = 10 
            hoistX = 20
            hoist_y = 20
            hoistZ =20
            connected = (math.sqrt((hoistX - containerX)**2), math.sqrt((hoist_y - containerY)**2), math.sqrt((hoistZ - containerZ)**2))
            if connected <= (10,10,10):
                isConnected = True
            else:
                print("to far away")
                isConnected = False
            print(connected)
            self.Hoist_data(isConnected)


    def Hoist_data(self, speed, pos):  
        data = {
            "meta":
                {
                    "topic": "crane/components/hoist/state",
                    "isActive": self.is_active,
                    "component": "hoist"
                },
            "msg": {
                "isConnected": "isConnected",
                "relativePosition": {
                    "HoistY": pos
                },
                "speed": {
                    "activeAcceleration": {
                        "y": True
                    },
                    "acceleration": {
                        "y": self.acceleration
                    },
                    "speed": {
                        "y": speed
                    }
                }
            }
        }
        print(data)
        self.client.publish("crane/components/hoist/state", data)
        self.client.disconnect()
Hoist()

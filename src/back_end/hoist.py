import broker.client
import random
import time
import json

class Hoist:
    def __init__(self):
        self.client = broker.client.Client("hoist", [("crane/components/hoist/command", 0)])
        self.client.serve()
        self.is_active = []
        self.acceleration = 1
        self.move = True
        self.frequency = 0.1
        self.hoist_y = 0
        self.positive_movement()

    def on_message(self, client, userdata, msg):
        print("Message received: ", msg.payload)
        data_dict = json.loads(msg.payload.decode('utf-8'))

        isConnected = data_dict.get('msg', {}).get('isConnected')
        self.is_active = data_dict.get('meta', {}).get('isActive')
        keys = data_dict.get('msg', {}).get('key')        

    broker.client.Client.on_message = on_message

    def positive_movement(self):
        while self.is_active is "true":
            time.sleep(self.frequency)

            self.acceleration = self.acceleration * 1.1
            speed = 0.83 * self.acceleration

            if speed >= 2.5:
                speed = 2.5
            # print(speed)
            move_hoist_y = speed
            self.position(move_hoist_y)

    def negative_movement(self):
        time.sleep(self.frequency)


        self.acceleration = self.acceleration * 1.1
        speed = -0.83 * self.acceleration

        if speed <= -2.5:
            speed = -2.5
        # print(speed)
        move_hoist_y = speed
        self.deceleration(speed)
        self.position(move_hoist_y)

    def deceleration(self, speed):
        while not self.move:
            time.sleep(self.frequency)
            if -0.5 <= speed <= 0.5:
                speed = 0
            elif speed >= 0.5:
                speed -= 0.2
            else:
                speed += 0.2
            self.position(speed)

    def position(self, speed):
        move_hoist_y = speed
        self.hoist_y += move_hoist_y
        pos = self.hoist_y
        max_pos = 40
        if pos > max_pos:
            pos = 40

        self.Hoist_data(speed, pos)
        # print(pos)

    def container_connect(self):
        is_connected = False
        can_connect = False
    
    def Hoist_data(self, speed, pos):  

        data = {
            "meta":
                {
                    "topic": "crane/components/hoist/state",
                    "isActive": True,
                    "component": "hoist"
                },
            "msg": {
                "isConnected": False,
                "relativePosition": {
                    "y": pos
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
        # self.client.publish("crane/components/hoist/state", data)




    def serve(self):
        self.broker.serve()
        while self.active:
            time.sleep(self.frequency)

        self.client_broker.disconnect()


Hoist()
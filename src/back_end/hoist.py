import broker.client
import random
import time

class Hoist:
    def __init__(self):
        self.is_active = False
        self.client = broker.client.Client("hoist", [("crane/components/hoist/command", 0)])
        self.active = True
        self.Hoist_data()

    
    def Hoist_data(self):
        self.client.serve()
        while self.active:
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
                        "y": round(random.uniform(0.0, 10.0), 2)
                    },
                    "speed": {
                        "activeAcceleration": {
                            "y": True
                        },
                        "acceleration": {
                            "y": round(random.uniform(0.0, 10.0), 2)
                        },
                        "speed": {
                            "y": round(random.uniform(0.0, 10.0), 2)
                        }
                    }
                }
            }
            # self.client.publish("crane/components/hoist/state", data)
        self.client.disconnect()

    def position(self):
        start_hoist_y = 500
        move_hoist_y = 0  # placeholder totdat movement is gemaakt
        new_hoist_y = 0   # placeholder totdat movement is gemaakt

    def container_connect(self):
        # placeholders totdat de container interactie is gemaakt
        is_connected = False
        can_connect = False

    def movement(self):
        # placeholder totdat de movement is gemaakt
        speed = 0
        acceleration = 0
    
    def serve(self):
        self.broker.serve()
        while self.active:
            time.sleep(self.frequency)

        self.client_broker.disconnect()


Hoist()
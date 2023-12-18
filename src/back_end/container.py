# container.py
import json
import broker.client
import random
import time

# init the ShipContainer with weight, dimensions(0,0,0), position
class ShipContainer:
    def __init__(self, weight, dimensions, position):
        self.weight = weight
        self.dimensions = dimensions
        self.position = position
        self.is_lifted = False  # Initialize as not lifted
        self.errors = []  # List to store error messages
        self.client = broker.client.Client("hoist", [("crane/components/hoist/command", 0)])
        self.active = True
        self.container_data()

    # set the position of the container if it's not lifted
    def set_position(self, new_position):
        if not self.is_lifted:
            self.position = new_position
        else:
            self.errors.append("Error: Cannot change position while container is lifted")

    # attach the container to the Spreader if it's not already attached
    def attach(self):
        if not self.is_lifted:
            self.is_lifted = True
        else:
            self.errors.append("Error: Container is already attached")

    # detach the container from the Spreader if it's attached
    def detach(self):
        if self.is_lifted:
            self.is_lifted = False
        else:
            self.errors.append("Error: Container is not attached")

    # publish the current status of the container via MQTT
    def send_status_update(self):
        status = self.get_status()
        self.client.publish("container/id/state", json.dumps(status))

    # get the current status of the container, including weight, dimensions, position, and attachment status
    def get_status(self):
        return {
            "weight": self.weight,
            "dimensions": self.dimensions,
            "position": self.position,
            "is_lifted": self.is_lifted,
            "errors": self.errors
        }
    
    def container_data(self):
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
            self.client.publish("crane/components/hoist/state", data)
        self.client.disconnect()

    def serve(self):
        self.broker.serve()
        while self.active:
            time.sleep(self.frequency)

        self.client_broker.disconnect()



ShipContainer(100, (10, 8, 6), (0, 0, 0))
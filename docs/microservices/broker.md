# Broker Client
This is a client that establishes a connection to a HiveMQ cluster.
As of now it can only subscribe and publish to one topic.

# Example implementation for the hoist micorservice
1.  Import the `broker` package 
    - From the `back_end` directory you can import the broker package with:
    ```python
    import broker.client
    ```
2. Make an instance of the `Client` class:
   - This can be done with:
   ```python
   import broker.client
   
   publisher = broker.client.Client(microservice="hoist", topic=[("crane/components/hoist/state", 0)], subscribe=True) 
   ```
3. Now send the generated data to the broker
   - This can be done by envoking the publish method:
   ```python
   import broker.client
   
   class Hoist:
    def __init__(self):
        self.client = Client("hoist", [("crane/components/hoist/command", 0)])
        self.active = True
        self.simulate()

    def simulate(self):
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

   Hoist()
   ```

from broker.client import Client
import time
import random

client = Client(microservice="testdata", subscribe=False)

client.serve()
while True:
    time.sleep(0.1)
    test_data = {"meta":{"topic":"crane/state","isActive":True},"absolutePosition":{"x":round(random.uniform(0.0, 10.0), 2),"y":round(random.uniform(0.0, 10.0), 2),"z":round(random.uniform(0.0, 10.0), 2)},"components":[{"component":"hoist","isActive":True,"isConnecting":True,"isConnected":True,"absolutePosition":{"x":round(random.uniform(0.0, 10.0), 2),"y":round(random.uniform(0.0, 10.0), 2),"z":round(random.uniform(0.0, 10.0), 2)},"speed":{"activeAcceleration":{"y":True},"acceleration":{"y":round(random.uniform(0.0, 10.0), 2)},"speed":{"y":round(random.uniform(0.0, 10.0), 2)}}},{"component":"trolley","isActive":True,"absolutePosition":{"x":round(random.uniform(0.0, 10.0), 2),"y":round(random.uniform(0.0, 10.0), 2),"z":round(random.uniform(0.0, 10.0), 2)},"speed":{"activeAcceleration":{"x":True},"acceleration":{"x":round(random.uniform(0.0, 10.0), 2)},"speed":{"x":round(random.uniform(0.0, 10.0), 2)}}},{"component":"boom","isActive":True,"absolutePosition":{"x":round(random.uniform(0.0, 10.0), 2),"y":round(random.uniform(0.0, 10.0), 2),"z":round(random.uniform(0.0, 10.0), 2)},"speed":{"activeAcceleration":{"x":True,"y":True},"acceleration":{"x":3.5,"y":7.9},"speed":{"X":1.2,"y":5.7}}},{"component":"gantry","isActive":True,"absolutePosition":{"x":9,"y":3.5,"z":7.9},"speed":{"activeAcceleration":{"z":True},"acceleration":{"z":1.2},"speed":{"z":5.7}}}],"container":{"id":42,"isConnected":True,"absolutePosition":{"x":round(random.uniform(0.0, 10.0), 2),"y":round(random.uniform(0.0, 10.0), 2),"z":round(random.uniform(0.0, 10.0), 2)},"speed":{"speed":{"x":round(random.uniform(0.0, 10.0), 2),"y":round(random.uniform(0.0, 10.0), 2),"z":round(random.uniform(0.0, 10.0), 2)}}},"commands":[{"target":"crane/components/hoist/command","command":True}]}

    client.publish("crane/state", test_data)

client.disconnect()



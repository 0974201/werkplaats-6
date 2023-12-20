from broker.client import Client
import time
import json

client = Client(microservice="testdata", subscribe=False)

test_data = {"meta":{"topic":"crane/state","isActive":True},"absolutePosition":{"x":1.2,"y":5.7,"z":9},"components":[{"component":"hoist","isActive":True,"isConnecting":True,"isConnected":True,"absolutePosition":{"x":3.5,"y":7.9,"z":1.2},"speed":{"activeAcceleration":{"y":True},"acceleration":{"y":5.7},"speed":{"y":9}}},{"component":"trolley","isActive":True,"absolutePosition":{"x":2.3,"y":6.8,"z":8.9},"speed":{"activeAcceleration":{"x":True},"acceleration":{"x":4.6},"speed":{"x":7.9}}},{"component":"boom","isActive":True,"absolutePosition":{"x":1.2,"y":5.7,"z":9},"speed":{"activeAcceleration":{"x":True,"y":True},"acceleration":{"x":3.5,"y":7.9},"speed":{"X":1.2,"y":5.7}}},{"component":"gantry","isActive":True,"absolutePosition":{"x":9,"y":3.5,"z":7.9},"speed":{"activeAcceleration":{"z":True},"acceleration":{"z":1.2},"speed":{"z":5.7}}}],"container":{"id":42,"isConnected":True,"absolutePosition":{"x":5.7,"y":9,"z":3.5},"speed":{"speed":{"x":0,"y":0.3,"z":5}}},"commands":[{"target":"crane/components/hoist/command","command":1}]}

client.serve()
while True:
    time.sleep(0.1)
    client.publish("crane/state", test_data)

client.disconnect()



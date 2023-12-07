#
# Copyright 2021 HiveMQ GmbH
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#      http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#
import time
import paho.mqtt.client as paho
from paho import mqtt
import json

# setting callbacks for different events to see if it works, print the message etc.
def on_connect(client, userdata, flags, rc, properties=None):
    print("CONNACK received with code %s." % rc)

# with this callback you can see if your publish was successful
def on_publish(client, userdata, mid, properties=None):
    print("mid: " + str(mid))

# print which topic was subscribed to
def on_subscribe(client, userdata, mid, granted_qos, properties=None):
    print("Subscribed: " + str(mid) + " " + str(granted_qos))

# gets called when a message arives, 
def on_message(client, userdata, message):
    payload_string = message.payload.decode('utf-8')
    print(payload_string)
    #passes a call with payload_string to prosses data
    process_data(payload_string)


# code here
# these are examples
# will loop endlessly

# will send the data to mqtt
def sent_data(speed_value):
    speed = speed_value
    mqtt_sent = '{"isActive": true, "component": "Hoist", "acceleration": null, "speed":' + str(speed) + '}'
    print(mqtt_sent)
    client.publish("hoist", payload=mqtt_sent, qos=1)


# egg of hoist to sent speed
class Hoist:
    def hoist_speed(self, get_speed):
        current_speed = get_speed
        speed_value = current_speed
        sent_data(speed_value)


# prosses the data in useble parts (add all of the componets u need the dat of in here)
def process_data(payload_string):
    data = json.loads(payload_string)
    isActive = data.get("isActive")
    component = data.get("component")
    acceleration = data.get("acceleration")
    get_speed = data.get("speed")

    hoist_instance = Hoist()
    hoist_instance.hoist_speed(get_speed)

# using MQTT version 5 here, for 3.1.1: MQTTv311, 3.1: MQTTv31
# userdata is user defined data of any type, updated by user_data_set()
# client_id is the given name of the client
client = paho.Client(client_id="", userdata=None, protocol=paho.MQTTv5)
client.on_connect = on_connect

# enable TLS for secure connection
client.tls_set(tls_version=mqtt.client.ssl.PROTOCOL_TLS)
# set username and password
client.username_pw_set("HoistMqtt", "Hoist1234")
# connect to HiveMQ Cloud on port 8883 (default for MQTT)
client.connect("c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud", 8883)

# setting callbacks, use separate functions like above for better visibility
client.on_subscribe = on_subscribe
client.on_message = on_message
client.on_publish = on_publish

# subscribe to the picks u need
# make sure ur not subscribe to ur own component otherwise it will endlessly loop
client.subscribe("#", qos=1)


# a single publish, this can also be done in loops, etc.


# loop_forever for simplicity, here you need to stop the loop manually
# you can also use loop_start and loop_stop
client.loop_forever()
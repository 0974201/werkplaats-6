import paho.mqtt.client as paho
from paho import mqtt
from dotenv import load_dotenv
import os
import time
import json


class Client:
    def __init__(self, microservice="", topics=[("#", 0)], subscribe=True):
        self.connected = False
        self.subscribed = None
        self.microservice = microservice
        self.topics = topics
        self.topics.append(("meta/emergency_button", 2))
        self.subscribe = subscribe
        load_dotenv()
        self.env = {
            "username": os.getenv("BROKER_USERNAME"),
            "password": os.getenv("BROKER_PASSWORD"),
            "url": os.getenv("BROKER_URL"),
            "port": int(os.getenv("BROKER_PORT"))
        }
        self.client = paho.Client(client_id=microservice, userdata=None, protocol=paho.MQTTv5)
        self.set_callbacks()

    def disconnect(self):
        self.client.disconnect()

    def connect(self):
        def on_connect(client, userdata, flags, rc, properties=None, ):
            print(f"{self.microservice} connected to {self.env['url']}")
            self.connected = True

        self.client.on_connect = on_connect
        self.client.tls_set(tls_version=mqtt.client.ssl.PROTOCOL_TLS)
        self.client.username_pw_set(self.env["username"], self.env["password"])
        self.client.connect(self.env["url"], self.env["port"])

    def set_callbacks(self):
        def on_subscribe(client, userdata, mid, granted_qos, properties=None):
            print(f"{self.microservice} subscribed to {self.topics}")
            self.subscribed = True

        def on_message(client, userdata, msg):
            print(f"{self.microservice} received {msg.payload.decode('utf-8')} on {self.topics}")

        self.client.on_subscribe = on_subscribe
        self.client.on_message = on_message

    def publish(self, topic: str, msg: dict, qos=0):
        def on_publish(client, userdata, mid, properties=None):
            print(f"{self.microservice} published {msg} to {topic}")
            pass

        self.client.on_publish = on_publish
        self.client.publish(topic, json.dumps(msg), qos)

    def serve(self):
        self.connect()
        if self.subscribe:
            self.subscribed = False
            self.client.subscribe(self.topics)
        self.client.loop_start()

        while not self.subscribed and self.subscribed is not None:
            print(f"{self.microservice} subscribing to {self.topics}")
            time.sleep(0.5)

        while not self.connected:
            print(f"{self.microservice} connecting to {self.topics}")
            time.sleep(0.5)

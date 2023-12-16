import paho.mqtt.client as paho
from paho import mqtt
from dotenv import load_dotenv
import os
import json
import time


class Client:
    def __init__(self) -> None:
        self.env = self.get_env()
        self.client = paho.Client(client_id="admin", userdata=None, protocol=paho.MQTTv5)
        self.client.tls_set(tls_version=mqtt.client.ssl.PROTOCOL_TLS)
        self.client.username_pw_set(username=self.env["username"], password=self.env["password"])
        self.connect()

    def get_env(self):
        load_dotenv()
        return {
            "username": os.getenv("BROKER_USERNAME"),
            "password": os.getenv("BROKER_PASSWORD"),
            "url": os.getenv("BROKER_URL"),
            "port": int(os.getenv("BROKER_PORT"))
        }

    def connect(self):
        self.client.connect(host=self.env["url"], port=self.env["port"])
        self.client.loop_start()
        while not self.client.is_connected():
            time.sleep(0.1)

    def subscribe_one(self, topic: str, qos: int):
        self.client.subscribe(topic, qos)

    def publish_one(self, topic: str, payload: dict, qos: int):
        payload_str = json.dumps(payload)
        self.client.publish(topic, payload_str, qos)

    def stop_loop(self):
        self.client.loop_stop()


test_data = {
    "test": "test"
}

client = Client()
client.publish_one("crane/state", test_data, 1)
client.stop_loop()

from .database import client as db_client
from .broker import client as b_client
import time
import json


class DbMqttClient:
    def __init__(self, frequency: float, active=True):
        self.frequency = frequency
        self.active = active
        self.topics = [("crane/state", 1), ("meta/errors", 2)]
        self.client_database = db_client.Client()
        self.client_broker = b_client.Client(
            "db_mqtt_client",
            self.topics
        )
        self.set_callbacks()
        self.serve()

    def set_callbacks(self):
        def insert_document(client, userdata, msg):
            msg_object = json.loads(msg.payload.decode("utf-8"))
            if msg_object["meta"]["topic"] == "meta/emergency_button" and msg_object["msg"]["isPressed"]:
                print(f"db_mqtt_client acknowledged emergency button state True")
            print(f"db_mqtt_client received {msg.payload.decode('utf-8')} on {self.topics}")
            self.client_database.insert_document(msg_object)

        self.client_broker.client.on_message = insert_document

    def serve(self):
        self.client_broker.serve()
        while True:
            time.sleep(self.frequency)
            

        self.client_broker.disconnect()

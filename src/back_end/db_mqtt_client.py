import database.client
import broker.client
import time


class DbMqttClient:
    def __init__(self, frequency: float, active=True):
        self.frequency = frequency
        self.active = active
        self.client_database = database.client.Client()
        self.client_broker = broker.client.Client(
            "db_mqtt_client",
            [("crane/state", 1), ("meta/errors", 2)]
        )
        self.set_callbacks()
        self.serve()

    def set_callbacks(self):
        def insert_document(client, userdata, msg):
            self.client_database.insert_document(msg.payload.decode("utf-8"))

        self.client_broker.client.on_message = insert_document

    def serve(self):
        self.client_broker.serve()
        while self.active:
            time.sleep(self.frequency)

        self.client_broker.disconnect()

import database.client
import broker.temp_client


class DatabaseAPI:
    def __init__(self):
        self.client_database = database.client.Client()
        self.client_broker = broker.temp_client.Client()

    def insert_message(self, topic, qos):
        self.client_broker.connect()
        self.client_broker.subscribe_one(topic=topic, qos=qos)


database_api = DatabaseAPI()

database_api.loop()
database_api.insert_message("#", 1)

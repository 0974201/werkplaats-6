import database.client
import broker.temp_client


class DatabaseAPI:
    def __init__(self):
        self.client_database = database.client.Client()
        self.client_broker = broker.temp_client.Client()


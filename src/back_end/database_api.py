import database.client
import broker.client


class DatabaseAPI:
    def __init__(self):
        self.client_database = database.client.Client()
        self.client_broker = broker.client.client

DatabaseAPI()

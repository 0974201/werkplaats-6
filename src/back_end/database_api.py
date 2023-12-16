import database.client
import broker.client
import asyncio


class DatabaseAPI:
    def __init__(self, active=True):
        self.active = active
        self.client_database = database.client.Client()
        self.client_broker = broker.client.Client(
            "database_api",
            [("crane/state", 1)]
        )
        self.set_callbacks()
        self.serve()

    def set_callbacks(self):
        def insert_document(client, userdata, msg):
            insertion = asyncio.run(self.client_database.insert_document(msg.payload.decode("utf-8")))

        self.client_broker.client.on_message = insert_document

    def serve(self):
        self.client_broker.serve()
        while self.active:
            pass

        self.client_broker.disconnect()

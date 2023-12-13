import database.client


class DatabaseAPI:
    def __init__(self):
        self.client_db = database.client.Client()
        self.client_db.insert_document({"test": "tested"})


DatabaseAPI()

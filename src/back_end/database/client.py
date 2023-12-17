from pymongo import MongoClient
from datetime import datetime
import ast
import pytz
from dotenv import load_dotenv
import os


class Client:
    def __init__(self) -> None:
        load_dotenv()
        self.uri = os.getenv("DB_URI")
        self.certificate = os.path.join(os.path.dirname(__file__), "certificate/admin.pem")
        self.client = MongoClient(self.uri, tls=True, tlsCertificateKeyFile=self.certificate)
        self.database = self.client["st-2324-1-d-wx1-t2-2324-wx1-bear"]

    def insert_document(self, msg: str) -> dict:
        timezone = pytz.timezone("Europe/Amsterdam")
        datetime_insertion = datetime.now(timezone)

        msg_object = ast.literal_eval(msg)
        topic = msg_object["meta"]["topic"]

        formatted_datetime_insertion = datetime_insertion.strftime("%Y-%m-%d/%H:%M:%S")
        document = {
            "datetime": formatted_datetime_insertion,
            "msg": msg_object
        }
        insertion = {
            "inserted": bool,
            "document": object
        }
        try:
            insert_one_result = self.database[topic].insert_one(document)
            insertion["document_id"] = insert_one_result.inserted_id
            insertion["inserted"] = True
        except Exception as error:
            insertion["document_id"] = None
            insertion["inserted"] = False
            print(error)
        finally:
            return insertion

    def find_document(self, query=None):
        found_document = {
            "document": self.collection.find_one(filter=query)
        }
        return found_document

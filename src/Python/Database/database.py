from pymongo import MongoClient
from dotenv import load_dotenv
import os


def get_environment_variables():
    load_dotenv()
    return {
        "DATABASE_URL": os.getenv("DATABASE_URL")
    }


class Database:
    def __init__(self) -> None:
        # get environment variables
        self.environment_variables = get_environment_variables()
        # create and assign a client to the running mongod instance to property
        self.mongod_client = MongoClient(self.environment_variables["DATABASE_URL"])
        # assign, and create if doesn't exist, database to property
        self.db = self.mongod_client.database
        # assign, and create if doesn't exist, collection to property
        self.collection = self.db.collection

    def insert_document(self, document) -> dict:
        insertion = {
            "inserted": bool,
            "document": object
        }
        try:
            insert_one_result = self.collection.insert_one(document)
            insertion["document_id"] = insert_one_result.inserted_id
            insertion["inserted"] = True
        except:
            insertion["document_id"] = None
            insertion["inserted"] = False
        finally:
            return insertion

    def find_document(self, query=None):
        found_document = {
            "document": self.collection.find_one(filter=query)
        }
        return found_document

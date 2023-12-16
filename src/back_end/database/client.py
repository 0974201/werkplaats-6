import json

from pymongo import MongoClient
from dotenv import load_dotenv
import os
import asyncio


class Client:
    def __init__(self) -> None:
        load_dotenv()
        self.uri = os.getenv("DB_URI")
        self.certificate = os.path.join(os.path.dirname(__file__), "certificate/admin.pem")
        self.client = MongoClient(self.uri, tls=True, tlsCertificateKeyFile=self.certificate)
        self.database = self.client["st-2324-1-d-wx1-t2-2324-wx1-bear"]
        self.collection = self.database.crane_state

    async def insert_document(self, document) -> dict:
        document_document = {
            "document": document
        }
        self.collection.insert_one(document_document)
        insertion = {
            "inserted": bool,
            "document": object
        }
        try:
            task = asyncio.create_task(self.collection.insert_one(document))
            insert_one_result = await task
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

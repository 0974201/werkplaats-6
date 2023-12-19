import client
import unittest


class TestDatabase(unittest.TestCase):
    def setUp(self) -> None:
        self.database = client.Client()
        self.test_data = "{'meta': {'topic': 'test/database_client'}, 'msg': 'test'}"
        self.topic = "test_db_client"
        self.inserted_document_id = None
        self.insertion = self.database.insert_document(str(self.test_data))
        self.inserted_document_id = self.insertion["document_id"]

    def test_inserts_document(self) -> None:
        msg = "document not inserted"
        insertion = self.database.insert_document(self.test_data)
        self.assertIs(expr1=insertion["inserted"], expr2=True, msg=msg)

    def test_finds_document(self) -> None:
        if self.inserted_document_id is None:
            reason = "the document has not been inserted"
            self.skipTest(reason=reason)
        msg = "document not found"
        found_document = self.database.find_document(self.topic, self.inserted_document_id)
        self.assertIsNotNone(found_document["document"],  msg=msg)


if __name__ == "__main__":
    unittest.main()

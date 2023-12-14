import unittest
import client


class TestDatabase(unittest.TestCase):
    def setUp(self) -> None:
        self.database = client.Client()
        self.test_data = {
          "meta": {
                  "topic": "crane/state",
                  "isActive": True
          },
          "absolutePosition": {
            "x": 1600.00,
            "y": 100.00,
            "z": 1000.00
          },
          "container": {
              "id": 1234,
              "isConnected": True
            },
          "components": [
            {
              "component": "hoist",
              "isActive": True,
              "isConnected": True,
              "absolutePosition": {
                  "x": 970.20,
                  "y": 130.45,
                  "z": 1600.00
              },
              "speed": {
                  "activeAcceleration": {
                    "y": True
                  },
                  "acceleration": {
                    "y": 0.00
                  },
                  "speed": {
                    "y": 2.50
                  }
              }
            },
            {
              "component": "trolley",
              "isActive": True,
              "absolutePosition": {
                "x": 970.20,
                "y": 220.00,
                "z": 1600.00
               },
              "speed": {
                "activeAcceleration": {
                  "x": False
                },
                "acceleration": {
                  "x": -1.25
                },
                "speed": {
                  "x": 2.50
                }
              }
            },
            {
              "component": "boom",
              "isActive": True,
              "absolutePosition": {
                "x": 970.20,
                "y": 220.00,
                "z": 1600.00
              },
              "speed": {
                "activeAcceleration": {
                  "x": False,
                  "y": False
                },
                "acceleration": {
                  "x": 0.00,
                  "y": 0.00
                },
                "speed": {
                  "X": 0.00,
                  "y": 0.00
                }
              }
            },
            {
              "component": "gantry",
              "isActive": "bool",
              "absolutePosition": {
                "x": 1000.00,
                "y": 100.00,
                "z": 1600.00
              },
              "speed": {
                "activeAcceleration": {
                  "z": False
                },
                "acceleration": {
                 "z": 0.00
                },
                "speed": {
                  "z": 0.00
                }
              }
            }
          ]
        }
        self.inserted_document_id = None
        self.insertion = self.database.insert_document(self.test_data)
        self.inserted_document_id = self.insertion["document_id"]

    def test_inserts_document(self) -> None:
        msg = "document not inserted"
        insertion = self.database.insert_document(self.test_data)
        self.assertIs(expr1=insertion["inserted"], expr2=False, msg=msg)

    def test_finds_document(self) -> None:
        if self.inserted_document_id is None:
            reason = "the document has not been inserted"
            self.skipTest(reason=reason)
        msg = "document not found"
        found_document = self.database.find_document(self.inserted_document_id)
        self.assertIsNotNone(found_document["document"],  msg=msg)


if __name__ == "__main__":
    unittest.main()

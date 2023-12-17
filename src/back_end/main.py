from db_mqtt_client import DbMqttClient


if __name__ == "__main__":
    frequency = 0.1
    database_api = DbMqttClient(frequency)

from . import db_mqtt_client


if __name__ == "__main__":
    frequency = 0.1
    database_api = db_mqtt_client.DbMqttClient(frequency)
else:
    raise "running package as import is not supported"
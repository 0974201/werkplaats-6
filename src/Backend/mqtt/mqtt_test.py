import template_mqtt
import time


client= template_mqtt.client

class Hoist:
    def __init__(self):
        self.sub = "(test1), (test2)"
        self.data_generated = 1
        self.topic = "hoist"
        self.is_active = True

    def messages(self):
        while self.is_active:
            time.sleep(1) # zodat ik niet perongelijk 18000 berichten in 5 seconden naar de mqtt stuur
            self.data_generated+= 1
            print(self.data_generated)
            client.publish(self.topic, self.data_generated)
    
    def subscribe(self):
        client.subscribe(self.sub)
        print("suc6")

    def on_message(client, userdata, msg):
        print(msg.topic + " " + str(msg.qos) + " " + str(msg.payload))

client.on_message = Hoist.on_message
hoist_instance = Hoist()
hoist_instance.messages()
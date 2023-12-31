from broker.client import Client
from threading import Thread, ThreadError
import json


class ComponentsClient:
    def __init__(self):
        self.microservice = "crane_state_components"
        self.topics = [("crane/components/gantry", 0), ("crane/components/boom", 0), ("crane/components/trolley", 0), ("crane/components/hoist", 0)]
        self.client = Client(microservice=self.microservice, topics=self.topics)
        self.set_callbacks()

    def set_callbacks(self):
        def check_topic(client, userdata, msg):
            msg_object = json.loads(msg.payload.decode("utf-8"))
            match msg_object["meta"]["topic"]:
                case "crane/components/gantry/state":
                    self.convert_gantry_state(msg_object["msg"])
                case "crane/components/gantry/command":
                    self.convert_gantry_command(msg_object["msg"])
                case "crane/components/boom/state":
                    self.convert_boom_state(msg_object["msg"])
                case "crane/components/boom/command":
                    self.convert_boom_command(msg_object["msg"])
                case "crane/components/trolley/state":
                    self.covnert_trolley_state(msg_object["msg"])
                case "crane/components/trolley/command":
                    self.convert_trolley_command(msg_object["msg"])
                case "crane/components/hoist/state":
                    self.covnert_hoist_state(msg_object["msg"])
                case "crane/components/hoist/command":
                    self.convert_hoist_command(msg_object["msg"])
          
        self.client.on_message = check_topic

    def convert_gantry_state(data):
        # process data
        # return to crane serve method
        pass
    
    def covnert_gantry_command(data):
        # process data
        # return to crane serve method
        pass
    
    def convert_boom_state(data):
        # process data
        # return to crane serve method
        pass
    
    def convert_boom_command(data):
        # process data
        # return to crane serve method
        pass
    
    def convert_trolley_state(data):
        # process data
        # return to crane serve method
        pass
    
    def convert_trolley_command(data):
        # process data
        # return to crane serve method
        pass
    
    def convert_hoist_state(data):
        # process data
        # return to crane serve method
        pass
    
    def convert_hoist_command(data):
        # process data
        # return to crane serve method
        pass


class ContainerClient:
    def __init__(self, container_ID):
        self.microservice = "crane_state_container"
        self.topics = [(f"containers/{container_ID}/state", 0)]
        self.client = Client(microservice=self.microservice, topics=self.topics)
        self.set_callbacks()

    def set_callbacks(self):
        def on_msg(client, userdata, msg):
            msg_object = json.loads(msg.payload.decode("utf-8"))
            self.convert_emergency_button(msg_object)
    
    def convert_emergency_button(data):
        # process data
        # return to crane serve method
        pass


class EmergencyButtonClient:
    def __init__(self):
        self.microservice = "crane_state_emergency_button"
        self.topics = [("meta/emergency_button", 2)]
        self.client = Client(microservice=self.microservice, topics=self.topics)
        self.set_callbacks()

    def set_callbacks(self):
        def on_msg(client, userdata, msg):
            msg_object = json.loads(msg.payload.decode("utf-8"))
            self.convert_emergency_button(msg_object)
    
    def convert_emergency_button(data):
        # process data
        # return to crane serve method
        pass
            


class Crane:
    def __init__(self, frequency):
        self.frequency = frequency
        self.client = Client("crane", subscribe=False)
        self.components_client = ComponentsClient
        self.components_thread = Thread(target=self.components_client)
        self.container_client = ContainerClient
        self.container_thread = Thread(target=self.container_client, args=1)
        self.emergency_button_client = EmergencyButtonClient
        self.emergency_button_thread = Thread(target=self.container_client)

    def serve(self):
        self.components_thread.start()
        self.container_thread.start()
        self.emergency_button_thread.start()

        # loop
        # frequency
        # get data from threads and parse into final message
        # publish message


# Obstacles: classes through threads and sending data from threads
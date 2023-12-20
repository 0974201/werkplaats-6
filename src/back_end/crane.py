from broker.client import Client
from threading import Thread, ThreadError
import asyncio
import ast


class Crane:
    def __init__(self, frequency):
        self.frequency = frequency
        self.client = Client("crane")
        self.component_states =
        self.component_commands =
        self.container_state =
        self.emergency_button_state =

    def set_callbacks(self):
      def on_emergency_button(client, userdata, msg):
        i

class
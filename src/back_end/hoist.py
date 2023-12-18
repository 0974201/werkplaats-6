import broker.client 

publisher = broker.client.Client(microservice="hoist", topics="crane/components/hoist/state", qos=0, subscribe=True) 

class Hoist:
    def __init__(self):
        self.is_active = False

    def position(self):
        start_hoist_y = 500
        move_hoist_y = 0  # placeholder totdat movement is gemaakt
        new_hoist_y = 0   # placeholder totdat movement is gemaakt

    def container_connect(self):
        # placeholders totdat de container interactie is gemaakt
        is_connected = False
        can_connect = False

    def movement(self):
        # placeholder totdat de movement is gemaakt
        speed = 0
        acceleration = 0


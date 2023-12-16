class EmergencyButton:
    def __init__(self, topic, sender):
        self.topic = topic
        self.sender = sender

    def stop_system(self):
        """
        Stops the entire system when the emergency button is pressed.
        """
        print(f"Emergency button pressed! Stopping the system for topic '{self.topic}' by sender '{self.sender}'.")

# Test:
if __name__ == "__main__":
    emergency_button = EmergencyButton(topic="meta/emergency_button", sender="controller")
    emergency_button.stop_system()

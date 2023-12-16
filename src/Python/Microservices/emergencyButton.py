# class for the emergency button
class EmergencyButton:
    def __init__(self, topic, sender):
        self.topic = topic # The topic of the emergency message
        self.sender = sender # The sender of the emergency message

    # method to stop the system when the emergency button is pressed
    def stop_system(self):
        """
        Stops the entire system when the emergency button is pressed.
        """
        # Print a message
        print(f"Emergency button pressed! Stopping the system for topic '{self.topic}' by sender '{self.sender}'.")

# test the class
if __name__ == "__main__":
    # create an emergency button object with a topic and a sender
    emergency_button = EmergencyButton(topic="meta/emergency_button", sender="controller")
    # call the stop_system method
    emergency_button.stop_system()

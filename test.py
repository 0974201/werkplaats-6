result_array = []

# Example data from server
data_from_server = [
    {"msg_type": "test1", "value": 42},
    {"msg_type": "test2", "value": "Hello"}
    # ... (more messages)
]

# Initialize a dictionary to store messages temporarily
temp_data_storage = {}

# Loop through the data from the server
for data in data_from_server:
    msg_type = data.get("msg_type")

    # Store data in the temporary dictionary
    temp_data_storage[msg_type] = data

    # Check if both messages are received
    if "test1" in temp_data_storage and "test2" in temp_data_storage:
        # Combine the messages and add to the result array
        combined_message = {
            "test1": temp_data_storage["test1"]["value"],
            "test2": temp_data_storage["test2"]["value"]
            # Add more fields as needed
        }
        result_array.append(combined_message)

        # Clear the temporary storage for the next set of messages
        temp_data_storage = {}

# Output the result array
print(result_array)
import pylsl

def send_string_to_unity(string_data):
    # Create a stream outlet with a unique stream name and content type
    outlet = pylsl.StreamOutlet(pylsl.StreamInfo("UnityStringStream", "String", 1, 0, pylsl.cf_string))

    # Send the string data through the outlet
    outlet.push_sample([string_data])

while True:
    # Example usage
    string_to_send = "Hello Unity!"
    send_string_to_unity(string_to_send)

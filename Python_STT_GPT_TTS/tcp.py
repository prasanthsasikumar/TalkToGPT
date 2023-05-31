import socket

def send_string_to_unity(message):
    # Unity's IP address and port number
    unity_ip = '127.0.0.1'  # Replace with Unity's IP address
    unity_port = 12345  # Replace with Unity's port number

    # Create a TCP client socket
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    try:
        # Connect to Unity
        client_socket.connect((unity_ip, unity_port))
        print('Connected to Unity.')

        # Send the string message
        client_socket.sendall(message.encode())
        print('String sent to Unity:', message)

    except ConnectionRefusedError:
        print('Failed to connect to Unity. Make sure Unity is listening.')
    
    finally:
        # Close the socket connection
        client_socket.close()
        print('Socket connection closed.')


# Test the function
# message_to_send = "TPE_idle1"
# send_string_to_unity(message_to_send)

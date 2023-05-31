using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TPeople;

public class TCPReceiver : MonoBehaviour
{
    public int listenPort = 12345;  // The port number to listen for incoming connections
    private string animationState = "Idle";
    public string talkAnim, listenAnim;

    public GameObject charector;

    private TcpListener tcpListener;
    private bool isListening = false;

    private void Start()
    {
        // Start listening for incoming TCP connections
        StartListening();
    }

    private void Update()
    {
        // Check for incoming data when listening
        if (isListening && tcpListener.Pending())
        {
            // Accept the client connection
            TcpClient client = tcpListener.AcceptTcpClient();

            // Start a background thread to handle the client connection
            System.Threading.Thread clientThread = new System.Threading.Thread(() =>
            {
               animationState = HandleClientConnection(client);
            });
            clientThread.Start();
            
            print(animationState);
            charector.GetComponent<Playanimation>().playtheanimation(animationState);
        }
    }

    private void OnDestroy()
    {
        // Stop listening and close the listener when the script is destroyed
        StopListening();
    }

    private void StartListening()
    {
        try
        {
            // Create a TCP listener on the specified port
            tcpListener = new TcpListener(IPAddress.Any, listenPort);
            tcpListener.Start();
            isListening = true;

            Debug.Log("TCP listening started on port: " + listenPort);
        }
        catch (SocketException e)
        {
            Debug.LogError("Failed to start TCP listener: " + e.Message);
        }
    }

    private void StopListening()
    {
        if (isListening)
        {
            tcpListener.Stop();
            isListening = false;

            Debug.Log("TCP listening stopped.");
        }
    }

    private string HandleClientConnection(TcpClient client)
    {
        // Get the client's IP address and port
        string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        int clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port;

        Debug.Log("Accepted client connection from: " + clientIP + ":" + clientPort);

        // Read the incoming data from the client
        byte[] buffer = new byte[1024];
        int bytesRead = client.GetStream().Read(buffer, 0, buffer.Length);
        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Debug.Log("Received message from Python: " + receivedMessage);

        // Handle the received message as needed

        // Close the client connection
        client.Close();
        Debug.Log("Closed client connection: " + clientIP + ":" + clientPort);

        return receivedMessage;
    }
}

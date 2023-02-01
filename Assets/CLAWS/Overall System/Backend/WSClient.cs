using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

// https://github.com/sta/websocket-sharp C# websocket library
public class WSClient : MonoBehaviour
{
    // Thread safe queue to store messages
    public ConcurrentQueue<string> messageQueue;
    private WebSocket connection;

    private void Start()
    {
        string url1 = "ws://127.0.0.1:6969";
        connection = new WebSocket(url1);
        // Set behavior for this websocket when message is recieved
        connection.OnMessage += (sender, e) =>
        {
            OnMessage(e);
            Debug.Log("Message from MCC: " + e.Data);
        };
        connection.Connect();
        string message = "hello there";
        SendMCC(message);
    }

    // Update function (runs on main thread) continously checks queue for any new messages
    private void Update()
    {
        while (Simulation.User.AstronautTasks.messageQueue.TryDequeue(out string message))
        {
            JsonMessage readIn = JsonConvert.DeserializeObject<JsonMessage>(message);
            HandleMessage(readIn.message_type, message);
        }
    }
    // Sends message to the server
    private void SendMCC(string message)
    {
        connection.Send(message);
        Debug.Log("Sent: " + message);
    }

    private void HandleMessage(string messageType, string message)
    {
        //Do whatever
    }

    private void OnMessage(MessageEventArgs e)
    {
        Debug.Log("Inside WebsocektBehavior");
        Debug.Log(e.Data);
        messageQueue.Enqueue(e.Data);
    }
}


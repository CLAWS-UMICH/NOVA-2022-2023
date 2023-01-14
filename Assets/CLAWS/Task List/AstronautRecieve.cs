using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using TMPro;

public class RecieveMessage : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Debug.Log("Inside WebsocektBehavior");
        Debug.Log(e.Data);
        Simulation.User.AstronautTasks.messageQueue.Enqueue(e.Data);
    }
}
public class AstronautRecieve : MonoBehaviour
{
    private TcpListener server;
    public TMP_Text text;

    public string address;
    public int port;

    private void Start()
    {
        address = "127.0.0.1";
        port = 6969;
        var listenServer = new WebSocketServer("ws://127.0.0.1:6969");

        listenServer.AddWebSocketService<RecieveMessage>("/RecieveMessage");
        listenServer.Start();
        Debug.Log("WebSocket server is listening for incoming connections.");
    }

    private void Update()
    {
        while (Simulation.User.AstronautTasks.messageQueue.TryDequeue(out string message))
        {
            var msgJson = JObject.Parse(@message);
            string messageType = (string)msgJson["message_type"];
            HandleMessage(messageType, msgJson);
        }
    }

    private void HandleMessage(string messageType, JObject msgJson)
    {
        switch (messageType)
        {
            //Determine if the task is going to be appended or inserted
            case "task_list_updated":
                //Simulation.User.AstronautTasks.taskList
                Debug.Log("updatedTaskList");
                //TODO: Set values recieved from websocket to Simulation.User Astronaut class. 
                Simulation.User.AstronautTasks.tasksUpdated();
                EventBus.Publish<TasksUpdatedEvent>(new TasksUpdatedEvent());
                break;
        }
    }

    //Code below uses sockets and listens manually
    //private void RecieveFromMCC() { 		
    //    try {
    //        // Create listener on <address>:<port>.
    //        server = new TcpListener(IPAddress.Parse(address), port);
    //        server.Start();
    //        Debug.Log("WebSocket server is listening for incoming connections.");

    //        Byte[] bytes = new Byte[256];
    //        String message = null;

    //        while (true) {
    //            // Accept a new client, then open a stream for reading and writing.
    //            Debug.Log("WebSocket server is waiting for clients");
    //            TcpClient client = server.AcceptTcpClient();
    //            // Get a stream object for reading and writing
    //            NetworkStream stream = client.GetStream();
    //            // Loop to receive all the data sent by the client.
    //            int i;
    //            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
    //            {
    //                string addition = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
    //                Console.WriteLine("Received: {0}", addition);
    //                message += addition;
    //            }
    //            //Convert string into json object
    //            //var messageJson = JObject.Parse(message);
    //            //Handle message
    //            //string messageType = (string)messageJson["message_type"];
    //            HandleMessage(message);
    //            Debug.Log("afterhandle");
    //        }
    //    }
    //    catch (SocketException socketException) {
    //        Debug.Log("SocketException " + socketException.ToString());
    //    }
    //}
}

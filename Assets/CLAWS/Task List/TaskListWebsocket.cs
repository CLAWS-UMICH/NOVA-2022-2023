using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;

#if UNITY_WEBGL
using WebSocketSharp;
using WebSocketSharp.Server;
#endif

public class TaskListWebsocket : MonoBehaviour
{
    #if UNITY_WEBGL
    public string address;
    public int port;
    private WebSocket connection;

    private void Start()
    {
        address = "127.0.0.1";
        port = 6969;
        //string url = "ws://" + address + ":" + port;
        string url1 = "ws://127.0.0.1:6969";
        connection = new WebSocket(url1);
        // Set behavior for this websocket when message is recieved
        connection.OnMessage += (sender, e) =>
        {
            OnMessage(e);
            Debug.Log("MCC message: " + e.Data);
        };
        connection.Connect();
        connection.Send("hellobitch");
        Debug.Log("Connected to server");

        // Subscribe to task finished event
        EventBus.Subscribe<TaskCompletedEvent>(SendMCC);
    }

    // Continously checks if a new message has been recieved
    private void Update()
    {
        while (Simulation.User.AstronautTasks.messageQueue.TryDequeue(out string message))
        {
            JsonMessage readIn = JsonConvert.DeserializeObject<JsonMessage>(message);
            HandleMessage(readIn.message_type, message);
        }
    }

    private void SendMCC(TaskCompletedEvent e)
    {
        string message = "{\"message_type\":\"task_completed\",\"task_id\":" + e.taskID + "}";
        connection.Send(message);
        Debug.Log("Sent: " + message);
    }

    private void OnMessage(MessageEventArgs e)
    {
        Debug.Log("Inside WebsocektBehavior");
        Debug.Log(e.Data);
        Simulation.User.AstronautTasks.messageQueue.Enqueue(e.Data);
    }

    private void HandleMessage(string messageType, string message)
    {
        switch (messageType)
        {
            //Determine if the task is going to be appended or inserted
            case "task_list_updated":
                //Simulation.User.AstronautTasks.taskList
                Debug.Log("updatedTaskList");
                TaskListUpdated readIn = JsonConvert.DeserializeObject<TaskListUpdated>(message);
                Simulation.User.AstronautTasks.tasksUpdated(readIn.task_list);
                break;
        }
    }
    #endif
}

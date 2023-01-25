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
            JsonMessage readIn = JsonConvert.DeserializeObject<JsonMessage>(message);
            HandleMessage(readIn.message_type, message);
        }
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
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using TMPro;
using System.Linq;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;


public class MessageHandler: MonoBehaviour
{
    public ChatWindowInteractions chatWindow;

    private WebSocket connection;

    public string self = "Patrick";

    private void Start()
    {
        string url1 = "ws://35.2.32.122:4242";
        connection = new WebSocket(url1);
        // Set behavior for this websocket when message is recieved
        connection.OnMessage += (sender, e) =>
        {
            OnMessage(e);
            Debug.Log("MCC message: " + e.Data);
        };
        connection.Connect();
        connection.Send("{\"message_type\": \"registration\",\"username\": \"" + self + "\"}");
        Debug.Log("Connected to server");
    }

    // Continously checks if a new message has been recieved
    private void Update()
    {
        while (Simulation.User.AstronautMessaging.messageQueue.TryDequeue(out string message))
        {
            JsonMessage readIn = JsonConvert.DeserializeObject<JsonMessage>(message);
            HandleMessage(readIn.message_type, message);
        }
    }

    public void SendDM(int messageTemplate, string chatID, List<string> recipientSet)
    {
        recipientSet.Sort();
        string testmsg = "shitsfucked";
        //string content = "The missile knows where it is at all times. It knows this because it knows where it isn't. By subtracting where it is from where it isn't, or where it isn't from where it is (whichever is greater)"
        switch (messageTemplate)
        {
            case 0:
                testmsg = "Hello";
                break;
            case 1:
                testmsg = "Bitch";
                break;
            case 2:
                testmsg = "pls";
                break;
        }
        MessageJson message = new MessageJson();
        message.message_type = "dm";
        message.recipients = recipientSet;
        message.timeStamp = DateTime.Now.ToString("HH:mm");
        message.content = testmsg;
        message.sender = self;
        message.chatID = chatID;
        string jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
        connection.Send(jsonMessage);
        Debug.Log("Sent: " + message);
    }

    public void CreateGroupChat(string chatID, HashSet<string> recipientSet)
    {
        GroupClass message = new GroupClass();
        message.message_type = "create_group";
        message.chatID = chatID;
        message.recipients = recipientSet.ToList();
        message.recipients.Add(self);
        string jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
        connection.Send(jsonMessage);
        Debug.Log("Sent: " + message);
    }

    private void OnMessage(MessageEventArgs e)
    {
        Debug.Log(e.Data);
        Simulation.User.AstronautMessaging.messageQueue.Enqueue(e.Data);
    }

    private void HandleMessage(string messageType, string message)
    {
        switch (messageType)
        {
            case "dm":
                
                Debug.Log("recieved message");
                Message readin = JsonConvert.DeserializeObject<Message>(message);
                //create chatID
                //FIXME error HERE
                this.chatWindow.OnMessageRecieved(readin.chatID, readin);
                break;
            case "create_group":
                GroupClass group = JsonConvert.DeserializeObject<GroupClass>(message);
                Debug.Log(group.recipients);
                this.chatWindow.CreateGroup(group);
                break;
        }
    }

}

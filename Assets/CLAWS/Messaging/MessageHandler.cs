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
using System.Linq;
using System.Text.Json.Serialization;


public class MessageHandler: MonoBehaviour
{
    // Messaging Window related objects
    public GameObject MessagingWindow;
    //FIXME add scrolling parent
    // Add message prefab

    // Inbox Window related objects
    public GameObject InboxWindow;
    //FIXME add scrolling parent
    // Add chat prefab

    // Contact List Objects
    public GameObject ContactWindow;
    public TextMeshPro recipientNames;

    private WebSocket connection;
    public HashSet<string> recipientSet = new HashSet<string>();

    private void Start()
    {
        string url1 = "ws://127.0.0.1:4242";
        connection = new WebSocket(url1);
        // Set behavior for this websocket when message is recieved
        connection.OnMessage += (sender, e) =>
        {
            OnMessage(e);
            Debug.Log("MCC message: " + e.Data);
        };
        connection.Connect();
        connection.Send("{\"message_type\": \"registration\",\"username\": \"Joel\"}");
        Debug.Log("Connected to server");
    }

    // Continously checks if a new message has been recieved
    private void Update()
    {
        while (Simulation.User.AstronautMessaging.messageQueue.TryDequeue(out string message))
        {
            MessageJson readIn = JsonConvert.DeserializeObject<MessageJson>(message);
            HandleMessage(readIn.message_type, message);
        }
    }

    //FIXME
    //Chat priority function
    //Constantly order chats based on highest priority when event is hit
    public void UpdateChatPriorities()
    {

    }

    //FIXME
    //Chat to inbox button
    public void ChatInboxButton()
    {

    }

    //FIXME
    //Close current chat
    //Active inbox object
    //Rerender for most recent chats
    public void RenderIndoxWindoww()
    {

    }

    //Create new Chat
    public void CreateNewChat()
    {
        List<string> recipients = recipientSet.ToList();
        recipients.Sort();
        string chatID = string.Join("", recipients);
        if(Simulation.User.AstronautMessaging.chatLookup.ContainsKey(chatID))
        {
            // Pull up window with the existing chat rendered
            RenderChatWindow(chatID);
        }
        Chat newChat = new Chat(chatID, recipientSet);

        //Add to astronaut class
        Simulation.User.AstronautMessaging.chatList.Add(newChat);
        int index = Simulation.User.AstronautMessaging.chatList.Count() - 1;
        Simulation.User.AstronautMessaging.chatLookup[chatID] = index;
        //Close contact list window
        RenderChatWindow(chatID);
    }

    //FIXME
    public void RenderChatWindow(string chatID)
    {
       // Set Chat window active
       // Clear scrolling parent and create number of prefabs based on chat messages
       // Somehow start scrolling from the bottom of the list
    }

    //FIXME
    public void EditChatName(string customName)
    {

    }

    // Function to display contents of the recipient list
    public void RecipientHandler(string name)
    {
        if (!recipientSet.Contains(name))
        {
            recipientSet.Add(name);
        }
        else
        {
            recipientSet.Remove(name);
        }

        recipientNames.text = string.Join(", ", recipientSet.ToList());
        return;
    }

    public void SendDM(int messageTemplate)
    {
        string sender = "Joel";
        List<string> recipients = recipientSet.ToList();
        recipients.Sort();
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
        message.recipients = recipients;
        message.currentTime = DateTime.Now.ToString("HH:mm");
        message.content = testmsg;
        message.sender = sender;
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
                MessageJson readin = JsonConvert.DeserializeObject<MessageJson>(message);
                string printOut = "Sender: " + readin.sender + "\n" + "Message: " + readin.content + "\n" + "Time: " + readin.currentTime;
                break;
        }
    }

}

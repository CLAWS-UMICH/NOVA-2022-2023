using UnityEngine;
using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Messaging
{
    public ConcurrentQueue<string> messageQueue;
    public List<Chat> chatList;
    public List<int> priority; //FIXME find better datastructure for this
    //Key: chatID, Value: Index of the chat object in chatList
    public Dictionary<string, int> chatLookup;

    public Messaging()
    {
        messageQueue = new ConcurrentQueue<string>();
        chatList = new List<Chat>();
        priority = new List<int>();
        chatLookup = new Dictionary<string, int>();
    }
}
// Class presentation of a sent message within a chat
public class Message
{
    public string chatID;
    public string content;
    public string sender;
    public string timeStamp;

    public Message(string text, string sender, string timeStamp)
    {
        this.content = text;
        this.sender = sender;
        this.timeStamp = timeStamp;
    }
}
public class Chat
{
    public string title;
    public string chatID;
    public HashSet<string> members;
    public List<Message> messages;

    public Chat(string ID, HashSet<string> members)
    {
        this.chatID = ID;
        this.members = members;
        this.messages = new List<Message>();
        this.title = string.Join(", ", members.ToList());
        Debug.Log("HELOO");
        Debug.Log(this.title);
    }
}

public class MessageJson : JsonMessage
{
    public List<string> recipients;
    public string timeStamp;
    public string sender;
    public string content;
    public string chatID;
}
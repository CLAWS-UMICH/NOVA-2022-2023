using UnityEngine;
using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Messaging
{
    public ConcurrentQueue<string> messageQueue;
    public List<Chat> chatList;
    //Key: chatID, Value: Index of the chat object in chatList
    public Dictionary<string, int> chatLookup;

    public Messaging()
    {
        messageQueue = new ConcurrentQueue<string>();
        chatList = new List<Chat>();
        chatLookup = new Dictionary<string, int>();
    }
}
// Class presentation of a sent message within a chat
public class Message
{
    string text;
    string sender;
    DateTime timeStamp;
}
public class Chat
{
    string chatID;
    HashSet<string> members;
    List<Message> messages;
}

public class MessageJson
{
    public string message_type;
    public List<string> recipients;
    public string currentTime;
    public string sender;
    public string content;
}
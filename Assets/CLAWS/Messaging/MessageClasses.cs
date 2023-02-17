using UnityEngine;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Messaging
{
    public ConcurrentQueue<string> messageQueue;

    public Messaging()
    {
        messageQueue = new ConcurrentQueue<string>();
    }
}
public class Message
{
    string text;
    string sender;
}
public class Chat
{
    List<string> members;
    List<string> messages;
}

public class MessageJson
{
    public string message_type { get; set; }
    public string recipient { get; set; }
    public string content { get; set; }
}
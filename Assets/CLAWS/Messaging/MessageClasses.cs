using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
class Message
{
    string text;
    string sender;
}
class Chat
{
    List<string> members;
    List<string> messages;
}

class MessageJson
{
    public string message_type { get; set; }
    public string recipient { get; set; }
    public string content { get; set; }
}
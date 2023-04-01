using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;

public class QueueClass
{
    public ConcurrentQueue<string> messageQueue;
    public QueueClass()
    {
        messageQueue = new ConcurrentQueue<string>();
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

[System.Serializable]
public class TaskList
{
    public double progress;
    public List<TaskObj> taskList;
    public ConcurrentQueue<string> messageQueue;

    public TaskList()
    {
        messageQueue = new ConcurrentQueue<string>();
        taskList = new List<TaskObj>();
        progress = 0;
    }
    // This function will take the json data from a websocket and parse the data into the tasklist.
    public void tasksUpdated() {}
}


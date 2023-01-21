using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class TaskList : MonoBehaviour
{
    
    public double progress;
    //taskList holds all tasks including previously completed tasks and future tasks.
    public List<TaskObj> taskList = new List<TaskObj>();
    public ConcurrentQueue<string> messageQueue;


    public TaskList()
    {
        messageQueue = new ConcurrentQueue<string>();
        taskList = new List<TaskObj>();
        progress = 0;
    }
    // This function will take the json data from a websocket and parse the data into the tasklist.
    public void tasksUpdated(JObject json) {
        
    }
}


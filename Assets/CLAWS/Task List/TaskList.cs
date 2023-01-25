using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class TaskList
{
    public float progress;
    //taskList holds all tasks including previously completed tasks and future tasks.
    public List<TaskObj> taskList;
    public ConcurrentQueue<string> messageQueue;


    public TaskList()
    {
        messageQueue = new ConcurrentQueue<string>();
        taskList = new List<TaskObj>();
        progress = 0;
    }
    // This function will eventually check for current subtask progress and save it if applicable
    public void tasksUpdated(List<TaskObj> newList)
    {
        taskList = newList;
        int index = -1;
        for(int i = 0; i < taskList.Count; ++i)
        {
            //if (taskList[i].completed == false)
            //{

            //}
            if (taskList[i].taskType == 'c')
            {
                index = i;
                break;
            }
        }
        EventBus.Publish<TasksUpdatedEvent>(new TasksUpdatedEvent(index));
    }
}

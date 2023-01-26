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
    public int viewTask;

    public TaskList()
    {
        messageQueue = new ConcurrentQueue<string>();
        taskList = new List<TaskObj>();
        progress = 0;
        viewTask = 0;
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
                viewTask = i;
                break;
            }
        }
        EventBus.Publish<TasksUpdatedEvent>(new TasksUpdatedEvent(index));
    }

    public float getProgress()
    {
        float taskListSize = taskList.Count;
        if (taskListSize == 0)
        {
            return 0;
        }

        float completed_count = 0;
        for (int i = 0; i < taskListSize; ++i)
        {
            if (taskList[i].taskType == 'c')
            {
                ++completed_count;
            }
        }
        Debug.Log(completed_count);
        Debug.Log(taskListSize);
        Debug.Log(completed_count / taskListSize);
        return (completed_count / taskListSize) * 100f;
    }

    public string getProgressText()
    {
        int taskListSize = taskList.Count;
        if (taskListSize == 0)
        {
            return "0/" + taskListSize + " subtasks completed";
        }

        int completed_count = 0;
        for (int i = 0; i < taskListSize; ++i)
        {
            if (taskList[i].taskType == 'c')
            {
                ++completed_count;
            }
        }
        return completed_count + "/" + taskListSize + " subtasks completed";
    }
}

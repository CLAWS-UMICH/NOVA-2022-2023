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
        bool found = false;
        for(int i = 0; i < taskList.Count; ++i)
        {
            if (taskList[i].completed == true)
            {
                taskList[i].taskType = 'p';
            }
            else if (taskList[i].completed == false && found == false)
            {
                found = true;
                viewTask = i;
                taskList[i].taskType = 'c';
            }
            else
            {
                taskList[i].taskType = 'f';
            }
            // udpate subtask status
            for (int j = 0; j < taskList[i].subtaskList.Count; ++j)
            {
                if (j == 0)
                {
                    taskList[i].subtaskList[j].taskType = 'c';
                }
                else
                {
                    taskList[i].subtaskList[j].taskType = 'f';
                }
            }
        }
        EventBus.Publish<TasksUpdatedEvent>(new TasksUpdatedEvent(viewTask));
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
            if (taskList[i].completed)
            {
                ++completed_count;
            }
        }
        return (completed_count / taskListSize) * 100f;
    }

    public string getProgressText()
    {
        int taskListSize = taskList.Count;
        if (taskListSize == 0)
        {
            return "0/" + taskListSize + " tasks completed";
        }

        int completed_count = 0;
        for (int i = 0; i < taskListSize; ++i)
        {
            if (taskList[i].completed)
            {
                ++completed_count;
            }
        }
        return completed_count + "/" + taskListSize + " tasks completed";
    }
}

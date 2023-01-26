using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskObj
{
    public int taskId;
    public string taskTitle;
    public string taskDesc;
    public char taskType;
    public List<Subtask> subtaskList;
    //p-past task, c-current task, f-future task

    public TaskObj(int id, string title, string desc, List<Subtask> subList, char type)
    {
        taskId = id;
        taskTitle = title;
        taskDesc = desc;
        subtaskList = subList;
        taskType = type;
    }
    public TaskObj()
    {
        taskId = -1;
        taskTitle = null;
        taskDesc = null;
        subtaskList = null;
        taskType = '\0';
    }
}

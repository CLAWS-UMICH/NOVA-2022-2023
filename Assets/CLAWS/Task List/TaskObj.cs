using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TaskObj
{
    public int taskId;
    public string taskTitle;
    public string taskDesc;
    public List<Subtask> subtaskList;

    public TaskObj(int id, string title, string desc, List<Subtask> subList)
    {
        taskId = id;
        taskTitle = title;
        taskDesc = desc;
        subtaskList = subList;
    }
}

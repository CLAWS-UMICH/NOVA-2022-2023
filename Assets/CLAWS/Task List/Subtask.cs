using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Subtask
{
    public int subTaskId;
    public string title;
    public string description;
    public char taskType;

    public Subtask()
    {
        subTaskId = -1;
        title = null;
        description = null;
        taskType = '\0';
    }
    public Subtask(int ID, string title_in, string desc_in, char type_in)
    {
        subTaskId = ID;
        title = title_in;
        description = desc_in;
        taskType = type_in;
    }
}

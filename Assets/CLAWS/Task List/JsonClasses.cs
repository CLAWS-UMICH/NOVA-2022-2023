using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json.Linq;

[System.Serializable]
// Classes for JSONParse to map JSON messages to
public class JsonMessage
{
    public string message_type;
}

[System.Serializable]
public class TaskListUpdated : JsonMessage
{
    public List<TaskObj> task_list;
}

public class VitalsUpdated : JsonMessage
{
    //public List<Stats> vitals_list;
}

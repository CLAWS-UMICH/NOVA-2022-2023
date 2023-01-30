using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalsUpdatedEvent 
{
    public override string ToString() {
	    return "<VitalsUpdatedEvent>: vitals were updated";
    }
}
public class TasksUpdatedEvent 
{
    public int index;

    public TasksUpdatedEvent()
    {
        index = -1;
    }

    public TasksUpdatedEvent(int updateIndex)
    {
        index = updateIndex;
    }
    public override string ToString()
    {
        return "<TasksUpdatedEvent>: tasks were updated";
    }
}
public class TaskCompletedEvent
{
    public int taskID;


    public TaskCompletedEvent(int id)
    {
        taskID = id;
    }

    public override string ToString()
    {
        return "<TaskCompletedEvent>: tasks " + taskID + " was completed";
    }
}

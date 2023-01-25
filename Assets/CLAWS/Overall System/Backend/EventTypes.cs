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

    public TasksUpdatedEvent(int updateIndex)
    {
        index = updateIndex;
    }
    public override string ToString()
    {
        return "<TasksUpdatedEvent>: tasks were updated";
    }
}

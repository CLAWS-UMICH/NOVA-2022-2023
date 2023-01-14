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
    public override string ToString()
    {
        return "<TasksUpdatedEvent>: tasks were updated";
    }
}

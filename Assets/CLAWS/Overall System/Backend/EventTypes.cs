using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

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

public class VEGA_InputEvent
{
    public string input;

    public VEGA_InputEvent(string _input)
    {
        input = _input;
    }

    public override string ToString()
    {
        return "<VEGA_InputEvent>: " +  input;
    }
}


public class VEGA_OutputEvent
{
    public string output;

    public VEGA_OutputEvent(string _output)
    {
        output = _output;
    }

    public override string ToString()
    {
        return "<VEGA_OutputEvent>: " + output;
    }
}

public class GeoSampleUpdatedEvent
{
    public int index;

    public GeoSampleUpdatedEvent()
    {
        index = -1;
    }

    public GeoSampleUpdatedEvent(int updateIndex)
    {
        index = updateIndex;
    }
    public override string ToString()
    {
        return "<GeoSampleUpdatedEvent>: samples were updated";
    }
}

// Event for letting us know GPS data was received from the server
public class UpdatedGPSEvent
{
    
}

public class UIAMsgEvent
{
    public UIAMsgEvent()
    {
        Debug.Log("UIA Msg Event Created");
    }

    public override string ToString()
    {
        return "<UIAMsgEvent>: new UIA msg";
    }
}
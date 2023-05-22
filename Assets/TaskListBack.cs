using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListBack : MonoBehaviour
{
    public TaskCollapse taskcollapse;

    void Start()
    {
        EventBus.Subscribe<BackEvent>(Callback_back);
    }

    void Callback_back(BackEvent e)
    {
        if (e.screen == Screens.TaskList_CurrentTask)
        {
            taskcollapse.Toggle();
        }
    }
}

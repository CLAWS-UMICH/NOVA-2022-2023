using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListScroll : MonoBehaviour
{
    public TaskListController tasklistcontroller;

    void Start()
    {
        EventBus.Subscribe<ScrollEvent>(Callback_scroll);
    }

    void Callback_scroll(ScrollEvent e)
    {
        if (e.screen == Screens.TaskList)
        {
            if (e.direction == Direction.up)
            {
                tasklistcontroller.changeCurrentIndex(-1);
            }
            else if (e.direction == Direction.down)
            {
                tasklistcontroller.changeCurrentIndex(1);
            }
        }
    }
}

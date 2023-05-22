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
        Debug.Log("received");
        if (e.screen == Screens.TaskList)
        {
            Debug.Log("scrolling");
            if (e.direction == Direction.up)
            {
                tasklistcontroller.changeCurrentIndex(-1);
                Debug.Log("down");
            }
            else if (e.direction == Direction.down)
            {
                Debug.Log("up");
                tasklistcontroller.changeCurrentIndex(1);
            }
        }
    }
}

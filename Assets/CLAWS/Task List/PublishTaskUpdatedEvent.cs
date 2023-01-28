using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT IS USELESS AND IS ONLY FOR TESTING, YOU CAN DELETE IT IF YOU WANT
public class PublishTaskUpdatedEvent : MonoBehaviour
{
    public void updateTaskEvent() {
        EventBus.Publish<TasksUpdatedEvent>(new TasksUpdatedEvent(2));
    }
}

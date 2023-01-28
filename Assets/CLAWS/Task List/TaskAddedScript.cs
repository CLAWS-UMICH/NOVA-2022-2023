using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class TaskAddedScript : MonoBehaviour
{
    [SerializeField]
    GameObject Notification;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<TasksUpdatedEvent>(ShowNotification);        
    }
    void ShowNotification(TasksUpdatedEvent e) {
        Notification.SetActive(true);
        StartCoroutine(Wait5Seconds());
        Notification.SetActive(false);
    }
    IEnumerator Wait5Seconds() {
        yield return new WaitForSeconds(5);
    }

}

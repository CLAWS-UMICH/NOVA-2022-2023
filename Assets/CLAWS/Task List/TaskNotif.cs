using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskNotif : MonoBehaviour
{
    public GameObject notifWindow;
    private IEnumerator countDown;

    void Start()
    {
        notifWindow.SetActive(false);
        countDown = DismissNotif();
        EventBus.Subscribe<TasksUpdatedEvent>(notify);
    }

    private void notify(TasksUpdatedEvent e)
    {
        StartCoroutine(countDown);
    }

    IEnumerator DismissNotif()
    {
        notifWindow.SetActive(true);
        yield return new WaitForSeconds(3f);
        notifWindow.SetActive(false);
    }
}

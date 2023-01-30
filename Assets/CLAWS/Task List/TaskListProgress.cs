using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskListProgress : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private IEnumerator moveProgress;
    private float targetProgress;
    private bool running;
    public Slider slider;
    public TextMeshPro totalCompletedText;


    // Start is called before the first frame update
    void Start()
    {
        running = false;
        targetProgress = 0f;
        moveProgress = UpdateBar();
        EventBus.Subscribe<TasksUpdatedEvent>(UpdateProgress);
        EventBus.Subscribe<TaskCompletedEvent>(CompletedUpdate);
    }

    public void Refresh()
    {
        Debug.Log("Refresh");
        UpdateProgress(new TasksUpdatedEvent(1));
    }

    private void CompletedUpdate(TaskCompletedEvent e)
    {
        Debug.Log("CompleteUpdate");
        UpdateProgress(new TasksUpdatedEvent(1));
    }
    //Refresh progress on event
    private void UpdateProgress(TasksUpdatedEvent e)
    {
        Debug.Log("ProgressUpdate");
        targetProgress = Mathf.Clamp(Simulation.User.AstronautTasks.getProgress(), 0 , 100);
        Debug.Log(targetProgress);
        if (running == true)
        {
            Debug.Log("Stop corourtine");
            StopCoroutine("UpdateBar");
        }
        StartCoroutine("UpdateBar");
        Debug.Log("Coroutine end");
    }

    //Increment or decrements progress bar until minimal threshold is met
    IEnumerator UpdateBar()
    {
        running = true;
        Debug.Log("In updateBar");
        Debug.Log(Math.Abs(targetProgress - slider.value));
        while (Math.Abs(targetProgress - slider.value) > 3f)
        {
            Debug.Log("In whileloops");
            if (targetProgress > slider.value)
            {
                Debug.Log("Addprogress");
                slider.value += 1f;
                totalCompletedText.text = Simulation.User.AstronautTasks.getProgressText();
                yield return null;
            }
            else if (targetProgress < slider.value)
            {
                Debug.Log("Minusprogress");
                slider.value -= 1f;
                totalCompletedText.text = Simulation.User.AstronautTasks.getProgressText();
                yield return null;
            }
        }
        Debug.Log("Exiting");
    }
}

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
    private bool running;
    private float targetProgress;

    public Slider slider;
    public TextMeshPro totalCompletedText;


    // Start is called before the first frame update
    void Start()
    {
        running = false;
        targetProgress = 0f;
        moveProgress = UpdateBar();
        EventBus.Subscribe<TasksUpdatedEvent>(UpdateProgress);
    }

    public void Refresh()
    {
        UpdateProgress(new TasksUpdatedEvent(1));
    }

    private void UpdateProgress(TasksUpdatedEvent e)
    {
        targetProgress = Mathf.Clamp(Simulation.User.AstronautTasks.getProgress(), 0 , 100);
        if(running == true)
        {
            StopCoroutine(moveProgress);
        }
        StartCoroutine(moveProgress);
    }

    IEnumerator UpdateBar()
    {
        running = true;
        while(Math.Abs(targetProgress - slider.value) > 3f)
        {
            if (targetProgress > slider.value)
            {
                slider.value += 1f;
                totalCompletedText.text = Simulation.User.AstronautTasks.getProgressText();
                yield return null;
            }
            else if (targetProgress < slider.value)
            {
                slider.value -= 1f;
                totalCompletedText.text = Simulation.User.AstronautTasks.getProgressText();
                yield return null;
            }
        }
        running = false;
    }
}

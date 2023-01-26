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
        Debug.Log(targetProgress);
        if(running == true)
        {
            StopCoroutine(moveProgress);
        }
        StartCoroutine(moveProgress);
    }

    IEnumerator UpdateBar()
    {
        Debug.Log("incoroutine");
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

    //// Update is called once per frame
    //void Update()
    //{
    //    float progress = Simulation.User.AstronautTasks.getProgress();
    //    updateProgressBar(progress);
    //    //Smoothly updates slider percentage
    //    if (slider.value < targetProgress)
    //    {
    //        //Limits number to [0,1]
    //        slider.value += Mathf.Clamp(fillSpeed * Time.deltaTime, 0, 1);
    //        totalCompletedText.text = Simulation.User.AstronautTasks.getProgressText();
    //    }
    //    else
    //    {
    //        slider.value -= Mathf.Clamp(fillSpeed * Time.deltaTime, 0, 1);
    //        totalCompletedText.text = Simulation.User.AstronautTasks.getProgressText();
    //    }
    //}
}

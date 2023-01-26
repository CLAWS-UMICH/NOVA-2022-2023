using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskListProgress : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float targetProgress;
    private float fillSpeed;

    public Slider slider;
    public TextMeshPro totalCompletedText;


    // Start is called before the first frame update
    void Start()
    {
        fillSpeed = 0.5f;
        targetProgress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float progress = Simulation.User.AstronautTasks.getProgress();
        updateProgressBar(progress);
        //Smoothly updates slider percentage
        if (slider.value < targetProgress)
        {
            //Limits number to [0,1]
            slider.value += Mathf.Clamp(fillSpeed * Time.deltaTime, 0, 1);
            totalCompletedText.text = Simulation.User.AstronautTasks.getProgressText();
        }
        else
        {
            slider.value -= Mathf.Clamp(fillSpeed * Time.deltaTime, 0, 1);
            totalCompletedText.text = Simulation.User.AstronautTasks.getProgressText();
        }
    }

    public void updateProgressBar(float newPercent)
    {
        //Limits number to [0,1]
        targetProgress = Mathf.Clamp(newPercent, 0, 1);
    }
}

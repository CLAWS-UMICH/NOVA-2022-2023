using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TaskListProgress : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float targetProgress;
    private float fillSpeed;

    private Slider slider;
    public void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        fillSpeed = 0.5f;
        targetProgress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Smoothly updates slider percentage
        if (slider.value < targetProgress)
        {
            //Limits number to [0,1]
            slider.value += Mathf.Clamp(fillSpeed * Time.deltaTime, 0, 1);
        }
        else
        {
            slider.value -= Mathf.Clamp(fillSpeed * Time.deltaTime, 0, 1);
        }
    }
    public void updateProgressBar(float newPercent)
    {
        //Limits number to [0,1]
        targetProgress = Mathf.Clamp(newPercent, 0, 1);
    }
}

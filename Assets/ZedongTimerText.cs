using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ZedongTimerText : MonoBehaviour
{
     public TextMeshPro timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTimerText(int time)
    {
        int hours = time / 3600;
        time -= hours * 3600;
        int minutes = time / 60;
        time -= minutes * 60;
        int seconds = time;
        timerText.text = $"{formatTime(hours)}:{formatTime(minutes)}:{formatTime(seconds)}";
    }

    private string formatTime(int time)
    {
        string strTime = time.ToString();
        if (strTime.Length <= 1)
        {
            strTime = "0" + strTime;
        }
        return strTime;
    }
}
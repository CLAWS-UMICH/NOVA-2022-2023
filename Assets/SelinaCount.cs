using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelinaCount : MonoBehaviour
{
    float currentTime = 0f;
    int minuteNum = 0;
    int hrNum = 0;
    float StartTime = 0f;

    public TextMeshPro hour;
    public TextMeshPro minute;
    public TextMeshPro second;

    void Start()
    {
        currentTime = StartTime;
    }

    
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        second.text = currentTime.ToString("00");
        minute.text = minuteNum.ToString("00");
        hour.text = hrNum.ToString("00");

        if (currentTime.ToString("0") == "60") {
            currentTime = 0f;
            minuteNum += 1;
        }

        if (minuteNum == 60) {
            minuteNum = 0;
            hrNum += 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BohnettUI : MonoBehaviour
{

    public void SetTime(int timeInSeconds, int timeInMinutes, int timeInHours)
    {
        TextMeshPro clock = gameObject.GetComponentInChildren<TextMeshPro>();
        
        string seconds = LeadingZeroes(timeInSeconds);
        string minutes = LeadingZeroes(timeInMinutes);
        string hours = LeadingZeroes(timeInHours);

        clock.text = hours + ":" + minutes + ":" + seconds;
        
    }

    private string LeadingZeroes(int time)
    {
        if (time < 10)
        {
            return "0" + time.ToString();
        }
        return time.ToString();
    }

}

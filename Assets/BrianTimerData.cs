using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrianTimerData : MonoBehaviour
{
    private float seconds = 0f;
    private float minutes = 0f;
    private float hours = 0f;
    private int timePassed = 0;
    public string _ctext = "";
    private string hourStr = "";
    private string minStr = "";
    private string secStr = "";

    public string cText
    {
        get
        {
            return _ctext;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Runs if statement every 1 second
        if (Time.time >= timePassed)
        {
            timePassed += 1;

            // Gets seconds, minutes, and hours
            seconds = timePassed % 60;
            minutes = Mathf.Floor(timePassed / 60);
            hours = Mathf.Floor(timePassed / 3600);

            // Converts time into string values
            hourStr = hours.ToString();
            minStr = minutes.ToString();
            secStr = seconds.ToString();

            // If statements to add a 0 infront of single digit times
            if (hours < 10)
            {
                hourStr = "0" + hours.ToString();
            }

            if (minutes < 10)
            {
                minStr = "0" + minutes.ToString();
            }

            if (seconds < 10)
            {
                secStr = "0" + seconds.ToString();
            }

            // Format correctly
            _ctext = hourStr + ":" + minStr + ":" + secStr;
        }
    }


}

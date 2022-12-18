using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerDataR : MonoBehaviour
{
    int secondstime= 0;
    public TextMeshPro timeText;
    string displaytime;

    IEnumerator MyCoroutine()
    {

        while (true)
        {
            secondstime++;
            DisplayTime(secondstime);

            yield return new WaitForSeconds(1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    void DisplayTime(float secondspassed)
    {
        float hours = Mathf.FloorToInt(secondspassed / 3600);

        secondspassed -= hours * 3600;

        float minutes = Mathf.FloorToInt(secondspassed / 60);

        float seconds = Mathf.FloorToInt(secondspassed % 60);

        displaytime = Updatedtime(hours) + ":" + Updatedtime(minutes) + ":" + 
        Updatedtime(seconds);

        timeText.text = displaytime;
    }

    string Updatedtime(float time)
    {
        if (time < 10)
        {
            return "0" + time.ToString();
        }
        return time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

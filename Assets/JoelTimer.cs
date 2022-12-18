using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoelTimer : MonoBehaviour
{
    int sec = 0;
    string displayText;
    [SerializeField] private TextMeshPro message;
    IEnumerator MyCoroutine()
    {
        while (true)
        {
            ++sec;
            displayTime(sec);
            yield return new WaitForSeconds(1F);
        }
    }

    void displayTime(int sec)
    {
        int hour = (sec / 3600);
        sec -= hour * 3600;
        int min = (sec / 60);
        sec -= min * 60;
        displayText = checkString(hour) + ":" + checkString(min) + ":" + checkString(sec);
        message.text = displayText;
    }

    string checkString(int time)
    {
        if (time < 10)
        {
            return "0" + time.ToString();
        }
        else
        {
            return time.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

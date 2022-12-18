using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class AdhavTimer : MonoBehaviour
{
    public TMP_Text timerText;
    [SerializeField] float startTime;
    string time(int seconds) {
        int hours = seconds / 3600;
        int min = (seconds - (hours * 3600)) / 60;
        int sec = (seconds - (hours * 3600) - (min * 60));
        return hours.ToString("00") + ":" + min.ToString("00") + ":" + sec.ToString("00");
    }

    // Start is called before the first frame update
    void Start()
    {
        timerText.SetText("00:00:01");
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        timerText.SetText(time((int)startTime));
    }
}

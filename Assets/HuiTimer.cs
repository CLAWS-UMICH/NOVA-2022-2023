using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuiTimer : MonoBehaviour
{
    private int time { get; set; }
    private IEnumerator startTimer;
    private bool isRunning = false;
    private HuiTimerText timerTextController;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = StartTimer();
        timerTextController = GetComponent<HuiTimerText>();
    }

    public void pressTimer()
    {
        if (isRunning)
        {
            StopCoroutine(startTimer);
            isRunning = false;
        }
        else
        {
            StopAllCoroutines();
            startTimer = StartTimer();
            StartCoroutine(startTimer);
        }
    }

    IEnumerator StartTimer()
    {
        isRunning = true;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            time += 1;
            timerTextController.updateTimerText(time);
        }
    }
}

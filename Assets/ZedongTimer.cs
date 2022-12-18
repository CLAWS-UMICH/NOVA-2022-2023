using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ZedongTimer : MonoBehaviour
{
    private int time { get; set; }
    private IEnumerator startTimer;
    private bool isRunning = false;
    private ZedongTimerText timerTextController;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = StartTimer();
        StartCoroutine(startTimer);
        // Debug.Log("Start");
        timerTextController = GetComponent<ZedongTimerText>();
        // Debug.Log("end");
    }

    IEnumerator StartTimer()
    {
        isRunning = true;
        Debug.Log("true");
        while (true)
        {
            yield return new WaitForSeconds(1f);
            time += 1;
            Debug.Log("StartTimer");
            timerTextController.updateTimerText(time);
        }
    }
}

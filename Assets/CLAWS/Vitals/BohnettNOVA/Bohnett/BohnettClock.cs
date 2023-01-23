using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BohnettClock : MonoBehaviour
{

    private int timeInSeconds = 0;
    private int timeInMinutes = 0;
    private int timeInHours = 0;

    private bool hasStarted = false;

    private Coroutine updateTimeCoroutine;


    public void StartTimer()
    {
        if (!hasStarted)
        {
            updateTimeCoroutine = StartCoroutine(updateTime());
            hasStarted = true;
        } 
        
    }

    public void EndTimer()
    {
        if (updateTimeCoroutine != null)
        {
            StopCoroutine(updateTimeCoroutine);
            hasStarted = false;
        }
    }

    public void ResetTimer()
    {
        timeInSeconds = 0;
        timeInMinutes = 0;
        timeInHours = 0;
        gameObject.GetComponentInChildren<BohnettUI>().SetTime(timeInSeconds, timeInMinutes, timeInHours);
        EndTimer();
    }

    private IEnumerator updateTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeInSeconds += 1;
            updateCorrectSection(ref timeInSeconds, ref timeInMinutes);
            updateCorrectSection(ref timeInMinutes, ref timeInHours);
            gameObject.GetComponentInChildren<BohnettUI>().SetTime(timeInSeconds, timeInMinutes, timeInHours);
        }
        
    }

    private void updateCorrectSection(ref int amountOverflow, ref int amountToIncrement)
    {
        if (amountOverflow >= 60)
        {
            amountToIncrement += 1;
            amountOverflow = 0;
        }
    }
}

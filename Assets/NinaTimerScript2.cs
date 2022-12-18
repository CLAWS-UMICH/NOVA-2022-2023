using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinaTimerScript2 : MonoBehaviour
{
    public bool timerRunning = false;
    public IEnumerator timertimer(){
    while (timerRunning == true)
        {
            yield return new WaitForSeconds(.1f);
        }
    StartCoroutine(timertimer());
    } 
}

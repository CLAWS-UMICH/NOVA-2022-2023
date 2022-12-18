using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinaTimerScript1 : MonoBehaviour
{
    public bool timerRunning = false;
    
    public IEnumerator timertimer()
    {
        while (timerRunning == true)
        {
            Debug.Log("HIHIHIHI");
            yield return new WaitForSeconds(.1f);
        }
    } 

    private void Start() {
        StartCoroutine(timertimer());
    }

}

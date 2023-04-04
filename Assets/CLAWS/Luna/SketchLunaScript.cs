using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class SketchLunaScript : MonoBehaviour
{
    Coroutine lunaCoroutine;
    void Start()
    {
        EnterLunaMode();
    }
    IEnumerator LunaMove() {
        for(int i = 0; i < 100; i++) {
            gameObject.GetComponent<RadialView>().enabled = false;
            Debug.Log(i);
            yield return new WaitForSeconds(5f);
            gameObject.GetComponent<RadialView>().enabled = true;
            yield return new WaitForSeconds(1f);
        }
    }
    public void EnterLunaMode() {
        lunaCoroutine = StartCoroutine(LunaMove());
    }

}

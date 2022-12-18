using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErnestoEditTime : MonoBehaviour
{   
    [SerializeField] GameObject ErnestoGameObject;
    ErnestoCounter ernestoCounter;

    void Awake() {
        ernestoCounter = ErnestoGameObject.GetComponent<ErnestoCounter>();
    }
    public void PauseAndUnpause() {
        ernestoCounter.togglePause();
    }

    public void reset() {
        ernestoCounter.SetSeconds(0);
    }

    public void addTenSeconds() {
        float newTime = ernestoCounter.GetSeconds() + 10;
        ernestoCounter.SetSeconds(newTime);
    }

}

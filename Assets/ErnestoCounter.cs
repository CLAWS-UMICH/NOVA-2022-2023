using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ErnestoCounter : MonoBehaviour
{   
    [SerializeField]
    private TextMeshPro CurrentTime;

    private float _seconds = 0;
    private bool _is_running = true;

    // Update is called once per frame
    void Update()
    {   
        if (_is_running) {
            _seconds += 1 * Time.deltaTime;

            
        }

        // Use TimeSpan from system namespace to calculate the time for us.
        TimeSpan t = TimeSpan.FromSeconds(_seconds);

        string newTime = string.Format("{0:D2}:{1:D2}:{2:D2}", 
                        t.Hours, 
                        t.Minutes, 
                        t.Seconds);
        

        CurrentTime.text = newTime;
        
    }

    public void togglePause() {
        _is_running = !_is_running;
    }

    public float GetSeconds(){
        return _seconds;
    }
    public void SetSeconds(float newSeconds) {
        _seconds = newSeconds;
    }
}

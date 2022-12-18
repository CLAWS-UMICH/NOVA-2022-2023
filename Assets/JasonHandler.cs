using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class JasonHandler : MonoBehaviour
{
    public TextMeshPro text;
    private int seconds;
    private IEnumerator timer;
    private bool toggle;

    // Start is called before the first frame update
    void Start()
    {
        timer = TimerCoroutine();
        toggle = false;
    }

    // Update is called once per frame
    public void Timehandler()
    {
        if(!toggle) {
            toggle = true;
           StartCoroutine(timer);
        }
        else {
            toggle = false;
            StopCoroutine(timer);
        }
    }

    private void TextChange() {
        TimeSpan format = new(0, 0, seconds);
        text.text = format.ToString();
    }

    IEnumerator TimerCoroutine()
    {
        while(true) {
            yield return new WaitForSeconds(1);
            ++seconds;
            TextChange();
        }
    }
}

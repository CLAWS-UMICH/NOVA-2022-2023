using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KritiTimer : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator coroutine;
    private int seconds = 0;
    private int minutes = 0;
    private int hours = 0;
    [SerializeField] private TextMeshPro timer;
    void Start()
    {
        timer = gameObject.GetComponentInChildren<TextMeshPro>();
        coroutine = UpdateTime();
        StartCoroutine(coroutine);
    }
    private string Zeroes(int time){        
        if (time < 10)
        {
            return "0" + time.ToString();
        }
        else
            return time.ToString();
    }

    private IEnumerator UpdateTime(){
        while (true){
            yield return new WaitForSeconds(1);
            if(seconds==59){
                seconds = -1;
                minutes++;
            }
            seconds++;
            if(minutes==59){
                minutes = 0;
                hours++;
            }
            timer.text = Zeroes(hours) + ":" + Zeroes(minutes) + ":" + Zeroes(seconds);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

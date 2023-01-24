using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Changes the fill amount on the meter progress bar, change the threshold for different increments of movement
public class MeterFill : MonoBehaviour
{
    [SerializeField] int threshold = 10;
    

    public void SetFillOnMeter(int percentageNum)
    { 
        float fillAmount = (percentageNum - (percentageNum % threshold)) / 100f;
        gameObject.GetComponent<Image>().fillAmount = fillAmount;

    }

}

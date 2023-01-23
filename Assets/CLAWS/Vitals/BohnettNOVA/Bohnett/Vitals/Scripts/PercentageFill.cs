using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Eventually will run this only when the value of percentageValue changes
// when grabbed from the JSON

public class PercentageFill : MonoBehaviour
{
    [SerializeField] MeterFill meter = null;
    [SerializeField] TMP_Text percentageText = null;
    [SerializeField] [Range(0f, 100f)] int percentageValue;

    private void Update()
    {
        percentageText.text = percentageValue.ToString();
        meter.SetFillOnMeter(percentageValue); 
    }
}
